using System;
using GoodNeighborHouse.TimeCard.Data.Context;
using GoodNeighborHouse.TimeCard.Data.Entities;
using GoodNeighborHouse.TimeCard.Data.General;

namespace GoodNeighborHouse.TimeCard.Data.Repositories
{
    internal sealed class PunchRepository : AbstractRepository<Punch, Guid>, IPunchRepository
	{
		internal PunchRepository(IGNHContext context) : base(context)
		{
		}

		protected override IDatabaseSet<Punch> DbSet => Context.Punches;
	}
}