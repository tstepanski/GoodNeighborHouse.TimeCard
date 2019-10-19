using GoodNeighborHouse.TimeCard.General;
using PunchModel = GoodNeighborHouse.TimeCard.Web.Models.Punch;
using PunchEntity = GoodNeighborHouse.TimeCard.Data.Entities.Punch;
using GoodNeighborHouse.TimeCard.Data.Entities;
using GoodNeighborHouse.TimeCard.Web.Models;

namespace GoodNeighborHouse.TimeCard.Web.Converters
{
    public class PunchConverter : IConverter<PunchEntity, PunchModel>,
        IMapper<PunchModel, PunchEntity>
    {
        public PunchModel Convert(PunchEntity from)
        {
            return new PunchModel
            {
                PunchTime = from.PunchTime,
                CreatedAt = from.CreatedAt,
                CreatedBy = from.CreatedBy,
                IsClockIn = from.IsClockIn,
                IsDeleted = from.IsDeleted,
                LastUpdatedBy = from.LastUpdatedBy,
                UpdatedAt = from.UpdatedAt,
                VolunteerId = from.VolunteerId,
                Id = from.Id
            };
        }

        public void MapTo(PunchModel model, PunchEntity entity)
        {
            entity.PunchTime = model.PunchTime;
            entity.CreatedAt = model.CreatedAt;
            entity.CreatedBy = model.CreatedBy;
            entity.IsClockIn = model.IsClockIn;
            entity.IsDeleted = model.IsDeleted;
            entity.LastUpdatedBy = model.LastUpdatedBy;
            entity.UpdatedAt = model.UpdatedAt;
            entity.VolunteerId = model.VolunteerId;
            
        }
    }
}
