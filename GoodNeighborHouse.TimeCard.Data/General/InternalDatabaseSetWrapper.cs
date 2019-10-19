using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GoodNeighborHouse.TimeCard.Data.General
{
	[ExcludeFromCodeCoverage] // Wrapper
	internal sealed class DatabaseSetWrapper
	{
		internal static IDatabaseSet<TEntity> CreateFrom<TEntity>(Func<DbSet<TEntity>> getter) where TEntity : class
		{
			return new InternalDatabaseSetWrapper<TEntity>(getter);
		}

		private sealed class InternalDatabaseSetWrapper<TEntity> : IDatabaseSet<TEntity> where TEntity : class
		{
			private readonly Func<DbSet<TEntity>> _underlyingDatabaseSetGetter;

			internal InternalDatabaseSetWrapper(Func<DbSet<TEntity>> underlyingDatabaseSet)
			{
				_underlyingDatabaseSetGetter = underlyingDatabaseSet;
			}

			private DbSet<TEntity> UnderlyingDatabaseSet => _underlyingDatabaseSetGetter();

			public IEnumerator<TEntity> GetEnumerator()
			{
				return ((IEnumerable<TEntity>) UnderlyingDatabaseSet).GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IEnumerable) UnderlyingDatabaseSet).GetEnumerator();
			}

			public Type ElementType => ((IQueryable) UnderlyingDatabaseSet).ElementType;
			public Expression Expression => ((IQueryable) UnderlyingDatabaseSet).Expression;
			public IQueryProvider Provider => ((IQueryable) UnderlyingDatabaseSet).Provider;

			public TEntity Find<TKey>(TKey key)
			{
				return UnderlyingDatabaseSet.Find(key);
			}

			public ValueTask<TEntity> FindAsync(object[] keys, CancellationToken cancellationToken = default)
			{
				return UnderlyingDatabaseSet.FindAsync(keys, cancellationToken);
			}

			public void Add(TEntity entity)
			{
				UnderlyingDatabaseSet.Add(entity);
			}

			public async ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken = default)
			{
				await UnderlyingDatabaseSet.AddAsync(entity, cancellationToken);
			}

			public void AddRange(IEnumerable<TEntity> entities)
			{
				UnderlyingDatabaseSet.AddRange(entities);
			}

			public Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
			{
				return UnderlyingDatabaseSet.AddRangeAsync(entities, cancellationToken);
			}

			public void Update(TEntity entity)
			{
				UnderlyingDatabaseSet.Update(entity);
			}

			public void UpdateRange(IEnumerable<TEntity> entities)
			{
				UnderlyingDatabaseSet.UpdateRange(entities);
			}

			public void Remove(TEntity entity)
			{
				UnderlyingDatabaseSet.Remove(entity);
			}

			public void RemoveRange(IEnumerable<TEntity> entities)
			{
				UnderlyingDatabaseSet.RemoveRange(entities);
			}
		}
	}
}