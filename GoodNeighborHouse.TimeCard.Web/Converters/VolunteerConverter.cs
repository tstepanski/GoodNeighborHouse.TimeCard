using System;
using System.Collections.Generic;
using System.Linq;
using GoodNeighborHouse.TimeCard.General;
using GoodNeighborHouse.TimeCard.Web.Models;
using VolunteerModel = GoodNeighborHouse.TimeCard.Web.Models.Volunteer;
using VolunteerEntity = GoodNeighborHouse.TimeCard.Data.Entities.Volunteer;
using DepartmentModel = GoodNeighborHouse.TimeCard.Web.Models.Department;
using DepartmentEntity = GoodNeighborHouse.TimeCard.Data.Entities.Department;

namespace GoodNeighborHouse.TimeCard.Web.Converters
{
	internal sealed class VolunteerConverter : IConverter<VolunteerEntity, VolunteerModel>,
		IMapper<VolunteerModel, VolunteerEntity>
	{
		private readonly IConverter<DepartmentEntity, DepartmentModel> _departmentConverter;

		public VolunteerConverter(IConverter<DepartmentEntity, DepartmentModel> departmentConverter)
		{
			this._departmentConverter = departmentConverter;
		}

		public VolunteerModel Convert(VolunteerEntity entity)
		{
			return new VolunteerModel
			{
				Id = entity.Id,
				Username = entity.Username,
				FirstName = entity.FirstName,
				LastName = entity.LastName,
				IsPaid = entity.IsPaid,
				IsGroup = entity.IsGroup,
				SelectedOrganizationId = entity.OrganizationId,
				Departments = entity
					.DepartmentVolunteers
					.Select(department => department.Department)
					.Select(_departmentConverter.Convert)
					.Select(department => Selection<Guid>.CreateSelected(department.Id, department.Name))
					.ToList()
			};
		}

		public void MapTo(VolunteerModel businessObject, VolunteerEntity entity)
		{
			entity.FirstName = businessObject.FirstName;
			entity.LastName = businessObject.LastName;
			entity.Username = businessObject.Username;
			entity.IsPaid = businessObject.IsPaid;
			entity.IsGroup = businessObject.IsGroup;
			entity.OrganizationId = businessObject.SelectedOrganizationId;

			entity.DepartmentVolunteers ??= new List<Data.Entities.DepartmentVolunteer>();

			foreach (var department in businessObject.Departments.Where(selection => selection.Selected))
			{
				var alreadyMapped = entity
					.DepartmentVolunteers
					.Any(existingJoinRecord => existingJoinRecord.DepartmentId == department.Item);

				if (alreadyMapped)
				{
					continue;
				}

				var joinRecord = new Data.Entities.DepartmentVolunteer
				{
					Volunteer = entity,
					DepartmentId = department.Item
				};

				entity.DepartmentVolunteers.Add(joinRecord);
			}

			var toRemove = new LinkedList<Data.Entities.DepartmentVolunteer>();

			foreach (var joinRecord in entity.DepartmentVolunteers)
			{
				var mapped = businessObject.Departments.Any(department =>
					department.Item == joinRecord.DepartmentId && department.Selected);

				if (!mapped)
				{
					toRemove.AddFirst(joinRecord);
				}
			}

			foreach (var joinRecord in toRemove)
			{
				entity.DepartmentVolunteers.Remove(joinRecord);
			}
		}
	}
}