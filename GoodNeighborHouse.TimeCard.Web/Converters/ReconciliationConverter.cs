using GoodNeighborHouse.TimeCard.General;
using ReconciliationModel = GoodNeighborHouse.TimeCard.Web.Models.Reconciliation;
using ReconciliationEntity = GoodNeighborHouse.TimeCard.Data.Entities.Reconciliation;

namespace GoodNeighborHouse.TimeCard.Web.Converters
{
    internal sealed class ReconciliationConverter : IConverter<ReconciliationEntity, ReconciliationModel>,
        IMapper<ReconciliationModel, ReconciliationEntity>
    {
        public ReconciliationModel Convert(ReconciliationEntity from)
        {
            return new ReconciliationModel
            {
                PunchInId = from.PunchInId,
                PunchOutId = from.PunchOutId,
                ApprovedBy = from.ApprovedBy,
                ApprovedOn = from.ApprovedOn
            };
        }

        public void MapTo(ReconciliationModel model, ReconciliationEntity entity)
        {
            entity.PunchOutId = model.PunchOutId;
            entity.PunchInId = model.PunchInId;
            entity.ApprovedOn = model.ApprovedOn;
            entity.ApprovedBy = model.ApprovedBy;
        }
    }
}
