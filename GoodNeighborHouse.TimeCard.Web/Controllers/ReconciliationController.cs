using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using GoodNeighborHouse.TimeCard.Data.Repositories;
using GoodNeighborHouse.TimeCard.Data.UnitOfWork;
using GoodNeighborHouse.TimeCard.General;
using GoodNeighborHouse.TimeCard.Web.Models;
using Microsoft.AspNetCore.Mvc;
using ReconciliationEntity = GoodNeighborHouse.TimeCard.Data.Entities.Reconciliation;
using PunchModel = GoodNeighborHouse.TimeCard.Web.Models.Punch;
using PunchEntity = GoodNeighborHouse.TimeCard.Data.Entities.Punch;
using VolunteerModel = GoodNeighborHouse.TimeCard.Web.Models.Volunteer;
using VolunteerEntity = GoodNeighborHouse.TimeCard.Data.Entities.Volunteer;

namespace GoodNeighborHouse.TimeCard.Web.Controllers
{
	[Route(@"Recon")]
	public class ReconciliationController : Controller
	{
		private readonly IConverter<PunchEntity, PunchModel> _punchConverter;
		private readonly IConverter<VolunteerEntity, VolunteerModel> _volunteerConverter;

		private readonly IUnitOfWorkFactory _unitOfWorkFactory;

		public ReconciliationController(IUnitOfWorkFactory unitOfWorkFactory,
			IConverter<PunchEntity, PunchModel> punchConverter,
			IConverter<VolunteerEntity, VolunteerModel> volunteerConverter)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
			_punchConverter = punchConverter;
			_volunteerConverter = volunteerConverter;
		}

		[HttpGet(@"Start/{volunteerId}/{startInMillis}/{endInMillis}")]
		public async Task<IActionResult> Create([FromRoute] Guid volunteerId, [FromRoute] long startInMillis,
			[FromRoute] long endInMillis, CancellationToken cancellationToken = default)
		{
			var start = DateTimeOffset.FromUnixTimeMilliseconds(startInMillis).DateTime;
			var end = DateTimeOffset.FromUnixTimeMilliseconds(endInMillis).DateTime;

			var getPunchesTask = GetPunches(volunteerId, start, end, cancellationToken)
				.ToImmutableArrayAsync(cancellationToken);

			var getVolunteerTask = Task.Run(async () =>
			{
				using (var unitOfWork = _unitOfWorkFactory.CreateReadOnly())
				{
					var volunteer = await unitOfWork
						.GetRepository<IVolunteerRepository>()
						.GetAsync(volunteerId, cancellationToken);

					return _volunteerConverter.Convert(volunteer);
				}
			}, cancellationToken);

			await Task.WhenAll(getPunchesTask, getVolunteerTask);

			var model = new VolunteerAndPunches(getVolunteerTask.Result, getPunchesTask.Result);

			return View(@"Recon", model);
		}

		[HttpPost]
		public async Task<IActionResult> Save(Guid volunteerId,
			DateTime start, DateTime end, CancellationToken cancellationToken = default)
		{
			using (var unitOfWork = _unitOfWorkFactory.CreateReadWrite())
			{
				var punches = (await unitOfWork
						.GetRepository<IPunchRepository>()
						.GetAllForVolunteerInPeriod(volunteerId, start, end)
						.ToImmutableArrayAsync(cancellationToken))
					.Select(_punchConverter.Convert)
					.ToImmutableArray();

				var reconciliationRepository = unitOfWork.GetRepository<IReconciliationRepository>();

				reconciliationRepository.ClearAllForVolunteerInPeriod(volunteerId, start, end);

				var reconciliation = new ReconciliationEntity();
				var newRecs = new LinkedList<ReconciliationEntity>();

				foreach (var punch in punches.Where(p => !p.IsDeleted))
				{
					if (punch.IsClockIn)
					{
						reconciliation = new ReconciliationEntity
						{
							PunchInId = punch.Id,
							ApprovedBy = 0
						};
					}
					else
					{
						reconciliation.ApprovedOn = DateTime.Now;
						reconciliation.PunchOutId = punch.Id;
						reconciliation.Difference =
							(long) (reconciliation.PunchOut.PunchTime - reconciliation.PunchIn.PunchTime)
							.TotalMilliseconds;
						newRecs.AddFirst(reconciliation);
					}
				}

				await reconciliationRepository.AddAllAsync(newRecs, cancellationToken);

				return RedirectToAction(@"Create");
			}
		}

		private async IAsyncEnumerable<IPunch> GetPunches(Guid volunteerId, DateTime start, DateTime end,
			[EnumeratorCancellation] CancellationToken cancellationToken = default)
		{
			using (var unitOfWork = _unitOfWorkFactory.CreateReadOnly())
			{
				var punchesByDay = (await unitOfWork.GetRepository<IPunchRepository>()
						.GetAllForVolunteerInPeriod(volunteerId, start, end)
						.ToImmutableArrayAsync(cancellationToken))
					.Select(_punchConverter.Convert)
					.OrderBy(punch => punch.PunchTime)
					.GroupBy(punch => punch.PunchTime.Date);

				foreach (var punchesForDay in punchesByDay)
				{
					var punchedIn = false;

					foreach (var punch in punchesForDay)
					{
						var isClockIn = punch.IsClockIn;

						if (isClockIn == punchedIn)
						{
							yield return new MissingPunch
							{
								VolunteerId = volunteerId,
								IsClockIn = !isClockIn,
								ShouldBeBefore = punch.PunchTime
							};
						}

						yield return punch;

						punchedIn = isClockIn;
					}

					if (punchedIn)
					{
						yield return new MissingPunch
						{
							VolunteerId = volunteerId,
							IsClockIn = false,
							ShouldBeBefore = punchesForDay.Key.Date.AddDays(1).AddMinutes(-1)
						};
					}
				}
			}
		}
	}
}