using System;
using GoodNeighborHouse.TimeCard.Data.Context;
using GoodNeighborHouse.TimeCard.Data.Entities;
using GoodNeighborHouse.TimeCard.Data.General;

namespace GoodNeighborHouse.TimeCard.Data.Repositories
{
    internal sealed class OrganizationRepository : AbstractRepository<Organization, Guid>, IOrganizationRepository
    {
        internal OrganizationRepository(IGNHContext context) : base(context)
        {
        }

        protected override IDatabaseSet<Organization> DbSet => Context.Organizations;
    }
}
