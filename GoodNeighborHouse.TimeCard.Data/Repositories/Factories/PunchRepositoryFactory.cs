using GoodNeighborHouse.TimeCard.Data.Context;

namespace GoodNeighborHouse.TimeCard.Data.Repositories.Factories
{
	internal sealed class PunchRepositoryFactory : AbstractRepositoryFactory<IPunchRepository>
	{
		public override IPunchRepository Create(IGNHContext context)
		{
			return new PunchRepository(context);
		}
	}
}