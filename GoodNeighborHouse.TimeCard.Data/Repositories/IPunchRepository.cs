using System;
using System.Collections.Generic;
using GoodNeighborHouse.TimeCard.Data.Entities;

namespace GoodNeighborHouse.TimeCard.Data.Repositories
{
    public interface IPunchRepository : IFullRepository<Punch, Guid>
	{
        IAsyncEnumerable<Punch> GetAllForVolunteerInPeriod(Guid id, DateTime start, DateTime end);


    }
}