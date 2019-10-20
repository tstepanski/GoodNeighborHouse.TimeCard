using System;
using System.Collections.Generic;
using System.Linq;
using GoodNeighborHouse.TimeCard.Data.Context;
using GoodNeighborHouse.TimeCard.Data.Entities;
using GoodNeighborHouse.TimeCard.Data.General;
using Microsoft.EntityFrameworkCore;

namespace GoodNeighborHouse.TimeCard.Data.Repositories
{
	internal sealed class ReconciliationRepository : AbstractRepository<Reconciliation, Guid>, IReconciliationRepository
	{
		public ReconciliationRepository(IGNHContext context) : base(context)
		{
		}

		protected override IDatabaseSet<Reconciliation> DbSet => Context.Reconciliations;

		public IAsyncEnumerable<Reconciliation> GetAllForPeriod(DateTime start, DateTime end)
		{
			return DbSet
				.Where(reconciliation => reconciliation.PunchIn.PunchTime >= start &&
				                         reconciliation.PunchOut.PunchTime <= end)
				.AsAsyncEnumerable();
		}

        public void ClearAllForVolunteerInPeriod(Guid volunteerId, DateTime start, DateTime end) {
            var remove = DbSet
                .Where(reconciliation => reconciliation.PunchIn.PunchTime >= start &&
                                          reconciliation.PunchIn.VolunteerId == volunteerId &&
                                          reconciliation.PunchOut.PunchTime <= end &&
                                          reconciliation.PunchOut.VolunteerId == volunteerId);
            DbSet.RemoveRange(remove);
        }
	}
}