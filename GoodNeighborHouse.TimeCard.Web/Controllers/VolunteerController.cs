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
using OrganizationModel = GoodNeighborHouse.TimeCard.Web.Models.Organization;
using OrganizationEntity = GoodNeighborHouse.TimeCard.Data.Entities.Organization;
using DepartmentModel = GoodNeighborHouse.TimeCard.Web.Models.Department;
using DepartmentEntity = GoodNeighborHouse.TimeCard.Data.Entities.Department;
using GoodNeighborHouse.TimeCard.Web.Models;

namespace GoodNeighborHouse.TimeCard.Web.Controllers
{
	public sealed class VolunteerController : Controller
	{
		private readonly IUnitOfWorkFactory _unitOfWorkFactory;
		private readonly IConverter<VolunteerEntity, VolunteerModel> _volunteerConverter;
		private readonly IMapper<VolunteerModel, VolunteerEntity> _mapper;
        private readonly IConverter<OrganizationEntity, OrganizationModel> _organizationConverter;
        private readonly IConverter<DepartmentEntity, DepartmentModel> _departmentConverter;

        public VolunteerController(IUnitOfWorkFactory unitOfWorkFactory,
			IConverter<VolunteerEntity, VolunteerModel> volunteerConverter, IMapper<VolunteerModel, VolunteerEntity> mapper,
            IConverter<OrganizationEntity, OrganizationModel> organizationConverter,
            IConverter<DepartmentEntity, DepartmentModel> departmentConverter)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
			_volunteerConverter = volunteerConverter;
			_mapper = mapper;
            _organizationConverter = organizationConverter;
            _departmentConverter = departmentConverter;
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
					.Select(_volunteerConverter.Convert)
					.ToImmutableArray();

				return View(@"ViewAll", volunteers);
			}
		}

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken cancellationToken = default)
        {
            var getOrganizationsTask = Task.Run(async () =>
            {
                using (var unitOfWork = _unitOfWorkFactory.CreateReadOnly())
                {
                    return (await unitOfWork
                        .GetRepository<IOrganizationRepository>()
                        .GetAllAsync()
                        .ToImmutableArrayAsync(cancellationToken))
                        .Select(_organizationConverter.Convert)
                        .ToImmutableArray();
                }
            }, cancellationToken);

            var getDepartmentsTask = Task.Run(async () =>
            {
                using (var unitOfWork = _unitOfWorkFactory.CreateReadOnly())
                {
                    return (await unitOfWork
                        .GetRepository<IDepartmentRepository>()
                        .GetAllAsync()
                        .ToImmutableArrayAsync(cancellationToken))
                        .Select(department => Selection<Guid>.CreateUnselected(department.Id, department.Name))
                        .ToImmutableArray();
                }
            }, cancellationToken);

            await Task.WhenAll(getOrganizationsTask, getDepartmentsTask);

            var model = new VolunteerModel
            {
                Organizations = getOrganizationsTask.Result.ToList(),
                Departments = getDepartmentsTask.Result.ToList()
            };

            return View(@"Edit", model);
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

				volunteer = _volunteerConverter.Convert(entity);

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

                var volunteer = _volunteerConverter.Convert(entity);

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

				volunteer = _volunteerConverter.Convert(entity);

                return View(volunteer);
			}
		}
	}
}