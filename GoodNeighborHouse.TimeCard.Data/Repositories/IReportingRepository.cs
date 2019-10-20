using System;
using System.Collections.Generic;
using GoodNeighborHouse.TimeCard.Data.Entities;

namespace GoodNeighborHouse.TimeCard.Data.Repositories
{
	public interface IReportingRepository : IRepository
	{
		IAsyncEnumerable<VolunteerDailyReport> GetAllForPeriodAsync(DateTime start, DateTime end);
        IAsyncEnumerable<OrganizationYTDReport> GetYTDForOrgAsync(Guid orgId, int year);

    }
}