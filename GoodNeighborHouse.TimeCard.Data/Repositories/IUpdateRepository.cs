using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GoodNeighborHouse.TimeCard.Data.Repositories
{
	public interface IUpdateRepository<TEntity, TKey> : IGetRepository<TEntity, TKey>
		where TEntity : class
		where TKey : IEquatable<TKey>
	{
		void Update(TEntity entity);
		Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
		void UpdateAll(IEnumerable<TEntity> entities);
		Task UpdateAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
	}
}