using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GoodNeighborHouse.TimeCard.Data.Repositories
{
	public interface IDeleteRepository<TEntity, TKey> : IGetRepository<TEntity, TKey>
		where TEntity : class
		where TKey : IEquatable<TKey>
	{
		void Delete(TKey key);
		Task DeleteAsync(TKey key, CancellationToken cancellationToken = default);
		void Delete(TEntity entity);
		Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
		void DeleteAll(IEnumerable<TEntity> entities);
		Task DeleteAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
	}
}