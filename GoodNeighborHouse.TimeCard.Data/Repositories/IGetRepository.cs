using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GoodNeighborHouse.TimeCard.Data.Repositories
{
	public interface IGetRepository<TEntity, TKey> : IRepository<TEntity, TKey>
		where TEntity : class
		where TKey : IEquatable<TKey>
	{
		TEntity Get(TKey key);
		ValueTask<TEntity> GetAsync(TKey key, CancellationToken cancellationToken = default);
		IEnumerable<TEntity> GetAll();
		IAsyncEnumerable<TEntity> GetAllAsync();
	}
}