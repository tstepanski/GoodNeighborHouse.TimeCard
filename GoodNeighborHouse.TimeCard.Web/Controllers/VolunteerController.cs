using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNeighborHouse.TimeCard.Data.Repositories;
using GoodNeighborHouse.TimeCard.Data.UnitOfWork;
using GoodNeighborHouse.TimeCard.General;
using Microsoft.AspNetCore.Mvc;
using VolunteerModel = GoodNeighborHouse.TimeCard.Web.Models.Volunteer;
using VolunteerEntity = GoodNeighborHouse.TimeCard.Data.Entities.Volunteer;
using GoodNeighborHouse.TimeCard.Data.Entities;

namespace GoodNeighborHouse.TimeCard.Web.Controllers
{
	public sealed class VolunteerController : Controller
	{
		private readonly IUnitOfWorkFactory _unitOfWorkFactory;
		private readonly IConverter<VolunteerEntity, VolunteerModel> _converter;
		private readonly IMapper<VolunteerModel, VolunteerEntity> _mapper;

		public VolunteerController(IUnitOfWorkFactory unitOfWorkFactory,
			IConverter<VolunteerEntity, VolunteerModel> converter, IMapper<VolunteerModel, VolunteerEntity> mapper)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
			_converter = converter;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> ViewAll(CancellationToken cancellationToken = default)
		{
			using (var unitOfWork = _unitOfWorkFactory.CreateReadOnly())
			{
				var volunteers = (await unitOfWork
						.GetRepository<IVolunteerRepository>()
						.GetAllAsync()
						.ToImmutableArrayAsync(cancellationToken))
					.Select(_converter.Convert)
					.ToImmutableArray();

				return View(@"ViewAll", volunteers);
			}
		}

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken cancellationToken = default)
        {
            var getOrganizationsTask = Task.Run(async () =>
            {
                using (var organizationsUnitOfWork = _unitOfWorkFactory.CreateReadOnly())
                {
                    return await organizationsUnitOfWork
                        .GetRepository<IGetRepository<Organization, Guid>>()
                        .GetAllAsync()
                        .ToImmutableArrayAsync(cancellationToken);
                }
            }, cancellationToken);

            var getDepartmentsTask = Task.Run(async () =>
            {
                using (var organizationsUnitOfWork = _unitOfWorkFactory.CreateReadOnly())
                {
                    return await organizationsUnitOfWork
                        .GetRepository<IGetRepository<Department, Guid>>()
                        .GetAllAsync()
                        .ToImmutableArrayAsync(cancellationToken);
                }
            }, cancellationToken);

            await Task.WhenAll(getOrganizationsTask, getDepartmentsTask);

            var organizations = getOrganizationsTask.Result;
            var departments = getDepartmentsTask.Result;

            return View(@"Edit", new VolunteerModel());
        }

		[HttpPost]
		public async Task<IActionResult> Create(VolunteerModel volunteer, CancellationToken cancellationToken = default)
		{
			using (var unitOfWork = _unitOfWorkFactory.CreateReadWrite())
			{
				var repository = unitOfWork.GetRepository<IVolunteerRepository>();
				var entity = new VolunteerEntity();

				_mapper.MapTo(volunteer, entity);

				await repository.AddAsync(entity, cancellationToken);
				await unitOfWork.CommitAsync(cancellationToken);

				volunteer = _converter.Convert(entity);

				return View(@"Edit", volunteer);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Edit([FromRoute] Guid id, CancellationToken cancellationToken = default)
		{
			using (var unitOfWork = _unitOfWorkFactory.CreateReadOnly())
			{
				var repository = unitOfWork.GetRepository<IVolunteerRepository>();
				var entity = await repository.GetAsync(id, cancellationToken);

                if (entity == null)
                {
                    return NotFound();
                }

                var volunteer = _converter.Convert(entity);

				return View(volunteer);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Edit([FromBody] VolunteerModel volunteer,
			CancellationToken cancellationToken = default)
		{
			using (var unitOfWork = _unitOfWorkFactory.CreateReadWrite())
			{
				var repository = unitOfWork.GetRepository<IVolunteerRepository>();
				var entity = await repository.GetAsync(volunteer.Id, cancellationToken);

                if (entity == null)
                {
                    return NotFound();
                }

                _mapper.MapTo(volunteer, entity);

				await repository.UpdateAsync(entity, cancellationToken);
				await unitOfWork.CommitAsync(cancellationToken);

				volunteer = _converter.Convert(entity);

                return View(volunteer);
			}
		}
	}
}