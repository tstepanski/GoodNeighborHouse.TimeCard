using GoodNeighborHouse.TimeCard.Data.Context;

namespace GoodNeighborHouse.TimeCard.Data.Repositories.Factories
{
	internal sealed class DepartmentVolunteerRepositoryFactory : AbstractRepositoryFactory<IDepartmentVolunteerRepository>
	{
		public override IDepartmentVolunteerRepository Create(IGNHContext context)
		{
			return new DepartmentVolunteerRepository(context);
		}
	}
}