using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GoodNeighborHouse.TimeCard.Data.General
{
	public interface IDatabaseSet<TEntity> : IQueryable<TEntity>, IAsyncEnumerable<TEntity> where TEntity : class
	{
		TEntity Find<TKey>(TKey key);
		ValueTask<TEntity> FindAsync(object[] keys, CancellationToken cancellationToken = default);
		void Add(TEntity entity);
		ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken = default);
		void AddRange(IEnumerable<TEntity> entities);
		Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
		void Update(TEntity entity);
		void UpdateRange(IEnumerable<TEntity> entities);
		void Remove(TEntity entity);
		void RemoveRange(IEnumerable<TEntity> entities);
	}
}