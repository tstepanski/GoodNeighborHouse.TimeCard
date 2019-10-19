using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GoodNeighborHouse.TimeCard.Data.Repositories
{
	public interface IAddRepository<TEntity, TKey> : IGetRepository<TEntity, TKey>
		where TEntity : class
		where TKey : IEquatable<TKey>
	{
		void Add(TEntity entity);
		ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken = default);
		void AddAll(IEnumerable<TEntity> entities);
		Task AddAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
	}
}