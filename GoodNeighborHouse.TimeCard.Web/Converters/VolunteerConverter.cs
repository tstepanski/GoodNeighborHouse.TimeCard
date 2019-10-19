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
				Pin = entity.Pin,
				FirstName = entity.FirstName,
				LastName = entity.LastName,
				DateOfBirth = entity.DateOfBirth
			};
		}

		public void MapTo(VolunteerModel businessObject, VolunteerEntity entity)
		{
			entity.FirstName = businessObject.FirstName;
			entity.LastName = businessObject.LastName;
			entity.DateOfBirth = businessObject.DateOfBirth;
			entity.Pin = businessObject.Pin;
		}
	}
}