using GoodNeighborHouse.TimeCard.Data.Context;

namespace GoodNeighborHouse.TimeCard.Data.Repositories.Factories
{
	internal sealed class VolunteerRepositoryFactory : AbstractRepositoryFactory<IVolunteerRepository>
	{
		public override IVolunteerRepository Create(IGNHContext context)
		{
			return new VolunteerRepository(context);
		}
	}
}