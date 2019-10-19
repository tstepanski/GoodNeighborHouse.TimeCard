using System;

namespace GoodNeighborHouse.TimeCard.Data.Repositories
{
	public interface IRepository
	{
	}

	// ReSharper disable UnusedTypeParameter
	public interface IRepository<TEntity, TKey> : IRepository
		where TEntity : class
		where TKey : IEquatable<TKey>
	{
	}
	// ReSharper restore UnusedTypeParameter
}