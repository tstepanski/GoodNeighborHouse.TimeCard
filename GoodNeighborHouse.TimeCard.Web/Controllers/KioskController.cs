﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNeighborHouse.TimeCard.Data.Repositories;
using GoodNeighborHouse.TimeCard.Data.UnitOfWork;
using GoodNeighborHouse.TimeCard.General;
using GoodNeighborHouse.TimeCard.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Punch = GoodNeighborHouse.TimeCard.Data.Entities.Punch;
using VolunteerModel = GoodNeighborHouse.TimeCard.Web.Models.Volunteer;
using VolunteerEntity = GoodNeighborHouse.TimeCard.Data.Entities.Volunteer;

namespace GoodNeighborHouse.TimeCard.Web.Controllers
{
	public sealed class KioskController : Controller
	{
		private readonly IUnitOfWorkFactory _unitOfWorkFactory;
		private readonly IConverter<VolunteerEntity, VolunteerModel> _volunteerConverter;

		public KioskController(IUnitOfWorkFactory unitOfWorkFactory,
			IConverter<VolunteerEntity, VolunteerModel> volunteerConverter)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
			_volunteerConverter = volunteerConverter;
		}

		[HttpPost(@"User")]
		public async Task<IActionResult> SelectUser([FromBody] SelectUserModel model,
			CancellationToken cancellationToken = default)
		{
			var username = model.UserName.Trim();

			using (var unitOfWork = _unitOfWorkFactory.CreateReadOnly())
			{
				var repository = unitOfWork.GetRepository<IVolunteerRepository>();
				var entity = await repository.GetByUserNameAsync(username, cancellationToken);
				var volunteer = _volunteerConverter.Convert(entity);

				return View(@"Punch", volunteer);
			}
		}

		[HttpPost(@"PunchIn/{volunteerId}/{departmentId}")]
		public async Task<IActionResult> PunchIn([FromRoute] Guid volunteerId, [FromRoute] Guid departmentId,
			[FromQuery] int? quantity, CancellationToken cancellationToken = default)
		{
			using (var unitOfWork = _unitOfWorkFactory.CreateReadWrite())
			{
				var userRepository = unitOfWork.GetRepository<IVolunteerRepository>();
				var punchRepository = unitOfWork.GetRepository<IPunchRepository>();
				var volunteer = await userRepository.GetAsync(volunteerId, cancellationToken);

				quantity ??= 1;

				if (volunteer.IsGroup && quantity > 1)
				{
					throw new ArgumentException(@"Non-group volunteers may not specify a quantity of greater than 1");
				}

				var entity = new Punch
				{
					Quantity = (int) quantity,
					DepartmentId = departmentId,
					Volunteer = volunteer,
					CreatedAt = DateTime.Now,
					CreatedBy = volunteer.Username,
					IsDeleted = false,
					PunchTime = DateTime.Now,
					UpdatedAt = DateTime.Now,
					IsClockIn = true,
					LastUpdatedBy = volunteer.Username
				};

				await punchRepository.AddAsync(entity, cancellationToken);
				await unitOfWork.CommitAsync(cancellationToken);

				return Accepted();
			}
		}

		[HttpPost(@"PunchOut/{volunteerId}")]
		public async Task<IActionResult> PunchOut([FromRoute] Guid volunteerId,
			CancellationToken cancellationToken = default)
		{
			using (var unitOfWork = _unitOfWorkFactory.CreateReadWrite())
			{
				var userRepository = unitOfWork.GetRepository<IVolunteerRepository>();
				var punchRepository = unitOfWork.GetRepository<IPunchRepository>();
				var volunteer = await userRepository.GetAsync(volunteerId, cancellationToken);
				var departmentId = volunteer.DepartmentVolunteers.First().Department.Id;

				var entity = new Punch
				{
					Quantity = 0,
					DepartmentId = departmentId,
					Volunteer = volunteer,
					CreatedAt = DateTime.Now,
					CreatedBy = volunteer.Username,
					IsDeleted = false,
					PunchTime = DateTime.Now,
					UpdatedAt = DateTime.Now,
					IsClockIn = false,
					LastUpdatedBy = volunteer.Username
				};

				await punchRepository.AddAsync(entity, cancellationToken);
				await unitOfWork.CommitAsync(cancellationToken);

				return Accepted();
			}
		}
	}
}