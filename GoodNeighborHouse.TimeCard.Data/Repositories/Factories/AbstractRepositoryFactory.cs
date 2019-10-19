using System;
using GoodNeighborHouse.TimeCard.Data.Context;

namespace GoodNeighborHouse.TimeCard.Data.Repositories.Factories
{
	internal abstract class AbstractRepositoryFactory<TRepository> : IRepositoryFactory<TRepository>
		where TRepository : IRepository
	{
		public Type ApplicableRepositoryType { get; } = typeof(TRepository);

		public abstract TRepository Create(IGNHContext context);

		IRepository IRepositoryFactory.Create(IGNHContext context)
		{
			return Create(context);
		}
	}
}