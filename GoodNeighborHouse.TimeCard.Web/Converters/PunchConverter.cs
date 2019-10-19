using GoodNeighborHouse.TimeCard.General;
using PunchModel = GoodNeighborHouse.TimeCard.Web.Models.Punch;
using PunchEntity = GoodNeighborHouse.TimeCard.Data.Entities.Punch;

namespace GoodNeighborHouse.TimeCard.Web.Converters
{
	internal sealed class PunchConverter : IConverter<PunchEntity, PunchModel>,
		IMapper<PunchModel, PunchEntity>
	{
		public PunchModel Convert(PunchEntity entity)
		{
			return new PunchModel
            {
                Id = entity.Id,
				VolunteerId = entity.VolunteerId,
                DepartmentId = entity.DepartmentId,
                IsClockIn = entity.IsClockIn,
                PunchTime = entity.PunchTime,
                IsDeleted = entity.IsDeleted,
                CreatedBy = entity.CreatedBy,
                CreatedAt = entity.CreatedAt,
                LastUpdatedBy = entity.LastUpdatedBy,
                UpdatedAt = entity.UpdatedAt
            };
		}

		public void MapTo(PunchModel businessObject, PunchEntity entity)
		{
            entity.VolunteerId = businessObject.VolunteerId;
            entity.DepartmentId = businessObject.DepartmentId;
            entity.IsClockIn = businessObject.IsClockIn;
            entity.PunchTime = businessObject.PunchTime;
            entity.IsDeleted = businessObject.IsDeleted;
            entity.CreatedBy = businessObject.CreatedBy;
            entity.CreatedAt = businessObject.CreatedAt;
            entity.LastUpdatedBy = businessObject.LastUpdatedBy;
            entity.UpdatedAt = businessObject.UpdatedAt;
		}
	}
}