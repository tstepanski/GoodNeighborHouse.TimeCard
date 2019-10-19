using System;

namespace GoodNeighborHouse.TimeCard.Data.Repositories
{
	public interface IFullRepository<TEntity, TKey> : IAddRepository<TEntity, TKey>, IUpdateRepository<TEntity, TKey>,
		IDeleteRepository<TEntity, TKey>
		where TEntity : class
		where TKey : IEquatable<TKey>
	{
	}
}