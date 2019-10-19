using System;
using System.Threading;
using System.Threading.Tasks;
using GoodNeighborHouse.TimeCard.Data.Repositories;

namespace GoodNeighborHouse.TimeCard.Data.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		TRepository GetRepository<TRepository>() where TRepository : IRepository;
		void Commit();
		Task CommitAsync(CancellationToken cancellationToken = default);
	}
}