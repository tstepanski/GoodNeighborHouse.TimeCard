using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNeighborHouse.TimeCard.Data.Context;
using GoodNeighborHouse.TimeCard.Data.General;
using Microsoft.EntityFrameworkCore;

namespace GoodNeighborHouse.TimeCard.Data.Repositories
{
	internal abstract class AbstractRepository<TEntity, TKey> : IFullRepository<TEntity, TKey>
		where TEntity : class
		where TKey : IEquatable<TKey>
	{
		protected internal AbstractRepository(IGNHContext context)
		{
			Context = context;
		}

		protected IGNHContext Context { get; }
		protected abstract IDatabaseSet<TEntity> DbSet { get; }

		public virtual TEntity Get(TKey key)
		{
			return DbSet.Find(key);
		}

		public virtual ValueTask<TEntity> GetAsync(TKey key, CancellationToken cancellationToken = default)
		{
			return DbSet.FindAsync(new object[] {key}, cancellationToken);
		}

		public virtual IEnumerable<TEntity> GetAll()
		{
			return DbSet.AsEnumerable();
		}

		public virtual IAsyncEnumerable<TEntity> GetAllAsync()
		{
			return DbSet.AsAsyncEnumerable();
		}

		public virtual void Add(TEntity entity)
		{
			DbSet.Add(entity);
		}

		public virtual ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			return DbSet.AddAsync(entity, cancellationToken);
		}

		public virtual void AddAll(IEnumerable<TEntity> entities)
		{
			DbSet.AddRange(entities);
		}

		public virtual Task AddAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
		{
			return DbSet.AddRangeAsync(entities, cancellationToken);
		}

		public virtual void Update(TEntity entity)
		{
			DbSet.Update(entity);
		}

		public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			DbSet.Update(entity);

			return ThrowOrReturnCompleted(cancellationToken);
		}

		public virtual void UpdateAll(IEnumerable<TEntity> entities)
		{
			DbSet.UpdateRange(entities);
		}

		public virtual Task UpdateAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
		{
			DbSet.UpdateRange(entities);

			return ThrowOrReturnCompleted(cancellationToken);
		}

		public virtual void Delete(TKey key)
		{
			var entity = Get(key);

			DbSet.Remove(entity);
		}

		public virtual async Task DeleteAsync(TKey key, CancellationToken cancellationToken = default)
		{
			var entity = await GetAsync(key, cancellationToken);

			DbSet.Remove(entity);
		}

		public virtual void Delete([NotNull] TEntity entity)
		{
			DbSet.Remove(entity);
		}

		public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			DbSet.Remove(entity);

			return ThrowOrReturnCompleted(cancellationToken);
		}

		public virtual void DeleteAll([NotNull] IEnumerable<TEntity> entities)
		{
			DbSet.RemoveRange(entities);
		}

		public virtual Task DeleteAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
		{
			DbSet.RemoveRange(entities);

			return ThrowOrReturnCompleted(cancellationToken);
		}

		private static Task ThrowOrReturnCompleted(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			return Task.CompletedTask;
		}
	}
}