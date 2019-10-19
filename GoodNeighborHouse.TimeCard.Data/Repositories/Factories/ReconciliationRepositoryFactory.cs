using GoodNeighborHouse.TimeCard.Data.Context;

namespace GoodNeighborHouse.TimeCard.Data.Repositories.Factories
{
    internal sealed class ReconciliationRepositoryFactory : AbstractRepositoryFactory<IReconciliationRepository>
    {
        public override IReconciliationRepository Create(IGNHContext context)
        {
            return new ReconciliationRepository(context);
        }
    }
}
