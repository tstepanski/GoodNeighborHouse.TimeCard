using System;
using System.Collections.Generic;
using GoodNeighborHouse.TimeCard.Data.Entities;

namespace GoodNeighborHouse.TimeCard.Data.Repositories
{
	public interface IReconciliationRepository : IFullRepository<Reconciliation, Guid>
	{
		IAsyncEnumerable<Reconciliation> GetAllForPeriod(DateTime start, DateTime end);
	}
}