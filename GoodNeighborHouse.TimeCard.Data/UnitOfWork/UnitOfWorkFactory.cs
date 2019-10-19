using System.Collections.Generic;
using System.Collections.Immutable;
using GoodNeighborHouse.TimeCard.Data.Context;
using GoodNeighborHouse.TimeCard.Data.Repositories.Factories;

namespace GoodNeighborHouse.TimeCard.Data.UnitOfWork
{
	internal sealed class UnitOfWorkFactory : IUnitOfWorkFactory
	{
		private readonly IGNHContextFactory _contextFactory;
		private readonly IImmutableSet<IRepositoryFactory> _repositoryFactories;

		public UnitOfWorkFactory(IGNHContextFactory contextFactory,
			IEnumerable<IRepositoryFactory> repositoryFactories)
		{
			_contextFactory = contextFactory;
			_repositoryFactories = repositoryFactories.ToImmutableHashSet();
		}

		public IUnitOfWork CreateReadOnly()
		{
			return Create(true);
		}

		public IUnitOfWork CreateReadWrite()
		{
			return Create(false);
		}

		private IUnitOfWork Create(bool readOnly)
		{
			var locationsContext = _contextFactory.Create(readOnly);

			return new UnitOfWork(_repositoryFactories, locationsContext);
		}
	}
}