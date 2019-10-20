using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using GoodNeighborHouse.TimeCard.Data.Context;
using GoodNeighborHouse.TimeCard.Data.Entities;
using GoodNeighborHouse.TimeCard.Data.General;

namespace GoodNeighborHouse.TimeCard.Data.Repositories
{
    internal sealed class PunchRepository : AbstractRepository<Punch, Guid>, IPunchRepository
	{
		public PunchRepository(IGNHContext context) : base(context)
		{
		}

		protected override IDatabaseSet<Punch> DbSet => Context.Punches;

        public IAsyncEnumerable<Punch> GetAllForVolunteerInPeriod(Guid volunteerId, DateTime start, DateTime end)
        {
            return DbSet
                .Where(punch => punch.PunchTime >= start &&
                                         punch.PunchTime <= end &&
                                         punch.VolunteerId == volunteerId)
                .AsAsyncEnumerable();
        }
    }
}