using System;
using GoodNeighborHouse.TimeCard.Data.Context;

namespace GoodNeighborHouse.TimeCard.Data.Repositories.Factories
{
	public interface IRepositoryFactory
	{
		Type ApplicableRepositoryType { get; }
		IRepository Create(IGNHContext context);
	}

	public interface IRepositoryFactory<out TRepository> : IRepositoryFactory where TRepository : IRepository
	{
		new TRepository Create(IGNHContext context);
	}
}