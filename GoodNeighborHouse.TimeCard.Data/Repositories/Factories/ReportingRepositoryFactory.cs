using GoodNeighborHouse.TimeCard.Data.Context;

namespace GoodNeighborHouse.TimeCard.Data.Repositories.Factories
{
	internal sealed class ReportingRepositoryFactory : AbstractRepositoryFactory<IReportingRepository>
	{
		public override IReportingRepository Create(IGNHContext context)
		{
			return new ReportingRepository(context);
		}
	}
}