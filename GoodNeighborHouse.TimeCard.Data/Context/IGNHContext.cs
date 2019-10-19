using System;
using System.Threading;
using System.Threading.Tasks;
using GoodNeighborHouse.TimeCard.Data.Entities;
using GoodNeighborHouse.TimeCard.Data.General;

namespace GoodNeighborHouse.TimeCard.Data.Context
{
	// ReSharper disable once InconsistentNaming
	public interface IGNHContext : IDisposable
	{
		IDatabaseSet<Volunteer> Volunteers { get; }
		void SaveChanges();
		Task SaveChangesAsync(CancellationToken cancellationToken = default);
		Task CreateDatabaseIfNeededAsync(CancellationToken cancellationToken = default);
		Task GetIfCanConnectAsync(CancellationToken cancellationToken = default);
	}
}