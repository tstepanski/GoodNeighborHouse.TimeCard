using GoodNeighborHouse.TimeCard.General;
using VolunteerModel = GoodNeighborHouse.TimeCard.Web.Models.Volunteer;
using VolunteerEntity = GoodNeighborHouse.TimeCard.Data.Entities.Volunteer;
using System.Linq;
using System.Collections.Generic;

namespace GoodNeighborHouse.TimeCard.Web.Converters
{
	internal sealed class VolunteerConverter : IConverter<VolunteerEntity, VolunteerModel>,
		IMapper<VolunteerModel, VolunteerEntity>
	{
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
                SelectedOrganizationId = entity.OrganizationId
                
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

            entity.DepartmentVolunteers = entity.DepartmentVolunteers ?? new List<Data.Entities.DepartmentVolunteer>();

            foreach(var department in businessObject.Departments.Where(selection => selection.Selected))
            {
                var alreadyMapped = entity
                    .DepartmentVolunteers
                    .Any(joinRecord => joinRecord.DepartmentId == department.Item);

                if (!alreadyMapped)
                {
                    var joinRecord = new Data.Entities.DepartmentVolunteer
                    {
                        Volunteer = entity,
                        DepartmentId = department.Item
                    };

                    entity.DepartmentVolunteers.Add(joinRecord);
                }
            }

            var toRemove = new LinkedList<Data.Entities.DepartmentVolunteer>();

            foreach(var joinRecord in entity.DepartmentVolunteers)
            {
                var mapped = businessObject.Departments.Any(department => department.Item == joinRecord.DepartmentId && department.Selected);
                
                if (!mapped)
                {
                    toRemove.AddFirst(joinRecord);
                }
            }

            foreach(var joinRecord in toRemove)
            {
                entity.DepartmentVolunteers.Remove(joinRecord);
            }
		}
	}
}