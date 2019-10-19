using GoodNeighborHouse.TimeCard.General;
using VolunteerModel = GoodNeighborHouse.TimeCard.Web.Models.Volunteer;
using VolunteerEntity = GoodNeighborHouse.TimeCard.Data.Entities.Volunteer;

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
				DateOfBirth = entity.DateOfBirth,
                IsPaid = entity.IsPaid,
                IsGroup = entity.IsGroup
			};
		}

		public void MapTo(VolunteerModel businessObject, VolunteerEntity entity)
		{
			entity.FirstName = businessObject.FirstName;
			entity.LastName = businessObject.LastName;
			entity.DateOfBirth = businessObject.DateOfBirth;
			entity.Username = businessObject.Username;
            entity.IsPaid = businessObject.IsPaid;
            entity.IsGroup = businessObject.IsGroup;
		}
	}
}