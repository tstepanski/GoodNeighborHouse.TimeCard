using System.Linq;
using GoodNeighborHouse.TimeCard.General;
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
			_departmentConverter = departmentConverter;
		}

		public VolunteerModel Convert(VolunteerEntity entity)
		{
			var departments = entity
				.DepartmentVolunteers
				.Select(department => department.Department)
				.Select(_departmentConverter.Convert)
				.ToList();

			return new VolunteerModel
			{
				Id = entity.Id,
				Username = entity.Username,
				FirstName = entity.FirstName,
				LastName = entity.LastName,
				IsPaid = entity.IsPaid,
				IsGroup = entity.IsGroup,
				OrganizationId = entity.OrganizationId,
				Departments = departments
			};
		}

		public void MapTo(VolunteerModel businessObject, VolunteerEntity entity)
		{
			entity.FirstName = businessObject.FirstName;
			entity.LastName = businessObject.LastName;
			entity.Username = businessObject.Username;
			entity.IsPaid = businessObject.IsPaid;
			entity.IsGroup = businessObject.IsGroup;
			entity.OrganizationId = businessObject.OrganizationId;
		}
	}
}