using System;
using GoodNeighborHouse.TimeCard.Data.Context;
using GoodNeighborHouse.TimeCard.Data.Entities;
using GoodNeighborHouse.TimeCard.Data.General;

namespace GoodNeighborHouse.TimeCard.Data.Repositories
{
    internal sealed class ReconciliationRepository : AbstractRepository<Reconciliation, Guid>, IReconciliationRepository
    {
        internal ReconciliationRepository(IGNHContext context) : base(context)
        {
        }
        protected override IDatabaseSet<Reconciliation> DbSet => Context.Reconciliations;
    }
}
