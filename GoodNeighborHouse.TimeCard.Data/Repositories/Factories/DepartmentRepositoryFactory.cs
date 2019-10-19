using GoodNeighborHouse.TimeCard.Data.Context;

namespace GoodNeighborHouse.TimeCard.Data.Repositories.Factories
{
    internal sealed class DepartmentRepositoryFactory : AbstractRepositoryFactory<IDepartmentRepository>
    {
        public override IDepartmentRepository Create(IGNHContext context)
        {
            return new DepartmentRepository(context);
        }
    }
}
