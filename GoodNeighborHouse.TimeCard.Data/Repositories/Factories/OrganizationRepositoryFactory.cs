using GoodNeighborHouse.TimeCard.Data.Context;

namespace GoodNeighborHouse.TimeCard.Data.Repositories.Factories
{
    internal sealed class OrganizationRepositoryFactory : AbstractRepositoryFactory<IOrganizationRepository>
    {
        public override IOrganizationRepository Create(IGNHContext context)
        {
            return new OrganizationRepository(context);
        }
    }
}