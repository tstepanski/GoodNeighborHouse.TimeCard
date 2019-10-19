using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using GoodNeighborHouse.TimeCard.Data.Context;
using GoodNeighborHouse.TimeCard.Data.Repositories;
using GoodNeighborHouse.TimeCard.Data.Repositories.Factories;

namespace GoodNeighborHouse.TimeCard.Data.UnitOfWork
{
	internal sealed class UnitOfWork : IUnitOfWork
	{
		private readonly IGNHContext _context;
		private readonly IImmutableDictionary<Type, IRepositoryFactory> _repositoryFactories;
		private readonly ConcurrentDictionary<Type, IRepository> _repositoryCache;

		public UnitOfWork(IEnumerable<IRepositoryFactory> repositoryFactories, IGNHContext context)
		{
			_context = context;

			_repositoryFactories = repositoryFactories
				.ToImmutableDictionary(factory => factory.ApplicableRepositoryType);

			_repositoryCache = new ConcurrentDictionary<Type, IRepository>();
		}

		public TRepository GetRepository<TRepository>() where TRepository : IRepository
		{
			var repositoryType = typeof(TRepository);

			if (!_repositoryFactories.ContainsKey(repositoryType))
			{
				throw new ArgumentException(@"Unsupported repository type");
			}

			if (_repositoryCache.ContainsKey(repositoryType))
			{
				return (TRepository) _repositoryCache[repositoryType];
			}

			var repositoryFactory = (IRepositoryFactory<TRepository>) _repositoryFactories[repositoryType];
			var repository = repositoryFactory.Create(_context);

			_repositoryCache[repositoryType] = repository;

			return repository;
		}

		public void Commit()
		{
			_context.SaveChanges();
		}

		public Task CommitAsync(CancellationToken cancellationToken = default)
		{
			return _context.SaveChangesAsync(cancellationToken);
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}