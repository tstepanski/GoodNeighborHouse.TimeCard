﻿using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNeighborHouse.TimeCard.Data.Entities;
using GoodNeighborHouse.TimeCard.Data.Repositories;
using GoodNeighborHouse.TimeCard.Data.UnitOfWork;
using GoodNeighborHouse.TimeCard.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolunteerModel = GoodNeighborHouse.TimeCard.Web.Models.Volunteer;
using VolunteerEntity = GoodNeighborHouse.TimeCard.Data.Entities.Volunteer;
using OrganizationModel = GoodNeighborHouse.TimeCard.Web.Models.Organization;
using OrganizationEntity = GoodNeighborHouse.TimeCard.Data.Entities.Organization;
using DepartmentModel = GoodNeighborHouse.TimeCard.Web.Models.Department;
using DepartmentEntity = GoodNeighborHouse.TimeCard.Data.Entities.Department;
using DepartmentVolunteer = GoodNeighborHouse.TimeCard.Data.Entities.DepartmentVolunteer;
using GoodNeighborHouse.TimeCard.Web.Models;
using System.Text.RegularExpressions;

namespace GoodNeighborHouse.TimeCard.Web.Controllers
{
	[Authorize]
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
            var getOrganizationsTask = GetOrganizationsTask(cancellationToken);

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
                Departments = getDepartmentsTask.Result.ToList(),
            };

            return View(@"Edit", model);
        }

		[HttpPost]
		public async Task<IActionResult> Create(VolunteerModel volunteer, CancellationToken cancellationToken = default)
		{
			using (var unitOfWork = _unitOfWorkFactory.CreateReadWrite())
			{  
				var repository = unitOfWork.GetRepository<IVolunteerRepository>();

                var firstName = volunteer.FirstName?.ToLower();
                var lastName = volunteer.LastName.ToLower();

                var previousVolunteerWithName = 
                    await repository.GetNewestVolunteerByName(volunteer.FirstName, volunteer.LastName, cancellationToken);

                string lastPart = string.Empty;

                if (previousVolunteerWithName != null)
                {
                    var previousUserName = previousVolunteerWithName.Username;
                    int index;
                    var lastNumber = 0;

                    for(index = 0; index < previousUserName.Length; index++)
                    {
                        if (Char.IsDigit(previousUserName[index]))
                        {
                            break;
                        }
                    }

                    int.TryParse(previousUserName.Substring(index), out lastNumber);
                    lastNumber++;
                    lastPart = lastNumber.ToString();
                }

                var firstPart = firstName == null ? string.Empty : $@"{firstName}.";
                var userName = Regex.Replace($@"{firstPart}{lastName}{lastPart}", @"\s", string.Empty);

                volunteer.Username = userName;

                var entity = new VolunteerEntity();

				_mapper.MapTo(volunteer, entity);

				await repository.AddAsync(entity, cancellationToken);
				await unitOfWork.CommitAsync(cancellationToken);

				return RedirectToAction(@"ViewAll");
			}
		}

		[HttpGet]
		public async Task<IActionResult> Edit([FromRoute] Guid id, CancellationToken cancellationToken = default)
		{
            var getVolunteer = GetVolunteer(id, cancellationToken);
            var getOrganizationsTask = GetOrganizationsTask(cancellationToken);
            var getAllDepartments = GetDepartmentsTask(cancellationToken);

            await Task.WhenAll(getVolunteer, getOrganizationsTask, getAllDepartments);

            var volunteer = getVolunteer.Result;

            volunteer.Organizations = getOrganizationsTask.Result.ToList();

            var currentlySelectedDepartments = volunteer
                .Departments
                .Select(department => department.Item)
                .ToImmutableSortedSet();

            volunteer.Departments = getAllDepartments
                .Result
                .Select(department => currentlySelectedDepartments.Contains(department.Id)
                    ? Selection<Guid>.CreateSelected(department.Id, department.Name)
                    : Selection<Guid>.CreateUnselected(department.Id, department.Name))
                .OrderBy(department => department.Display)
                .ToList();

            return View(volunteer);
        }

		[HttpPost]
		public async Task<IActionResult> Edit([FromForm] VolunteerModel volunteer,
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

                return RedirectToAction(@"ViewAll");
			}
		}

        #region Methods

        private async Task<ImmutableArray<OrganizationModel>> GetOrganizationsTask(CancellationToken cancellationToken = default)
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
        }

        private async Task<ImmutableArray<DepartmentModel>> GetDepartmentsTask(CancellationToken cancellationToken = default)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateReadWrite())
            {
                var repository = unitOfWork.GetRepository<IDepartmentRepository>();

                return (await repository
                    .GetAllAsync()
                    .ToImmutableArrayAsync(cancellationToken))
                    .Select(_departmentConverter.Convert)
                    .ToImmutableArray();
            }
        }

        private async Task<VolunteerModel> GetVolunteer(Guid id, CancellationToken cancellationToken = default)
        {
            var getVolunteer = Task.Run(async () =>
            {
                using (var unitOfWork = _unitOfWorkFactory.CreateReadOnly())
                {
                    var repository = unitOfWork.GetRepository<IVolunteerRepository>();
                    var entity = await repository.GetAsync(id, cancellationToken);

                    if (entity == null)
                    {
                        return null;
                    }

                    return _volunteerConverter.Convert(entity);
                }
            });

            return await getVolunteer;
        }

        #endregion
    }
}