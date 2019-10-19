using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNeighborHouse.TimeCard.Data.Entities;
using GoodNeighborHouse.TimeCard.Data.General;
using Microsoft.EntityFrameworkCore;

namespace GoodNeighborHouse.TimeCard.Data.Context
{
	public class GNHContext : DbContext, IGNHContext
	{
		private readonly bool _readOnly;
		private readonly IDatabaseSet<Volunteer> _volunteersWrapper;

		public GNHContext(DbContextOptions options, bool readOnly = false) : base(options)
		{
			_readOnly = readOnly;
			_volunteersWrapper = DatabaseSetWrapper.CreateFrom(() => Volunteers);
		}

#if DEBUG
		public GNHContext() : this(LocalDatabaseOptions.Instance.Options)
		{
		}
#endif

		public DbSet<Volunteer> Volunteers { get; set; }

		public new void SaveChanges()
		{
			ThrowIfReadOnly();

			base.SaveChanges();
		}

		public new Task SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			ThrowIfReadOnly();

			return base.SaveChangesAsync(cancellationToken);
		}

		public Task CreateDatabaseIfNeededAsync(CancellationToken cancellationToken = default)
		{
			return Database.MigrateAsync(cancellationToken);
		}

		public Task GetIfCanConnectAsync(CancellationToken cancellationToken = default)
		{
			return Database.CanConnectAsync(cancellationToken);
		}

		IDatabaseSet<Volunteer> IGNHContext.Volunteers => _volunteersWrapper;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var idProperties = modelBuilder
				.Model
				.GetEntityTypes()
				.Where(entityType => typeof(AbstractIdentifiable).IsAssignableFrom(entityType.ClrType))
				.Select(entityType => entityType.FindProperty(nameof(AbstractIdentifiable.ID)));

			foreach (var property in idProperties)
			{
				property.SetDefaultValueSql(@"NEWSEQUENTIALID()");
			}
		}

		private void ThrowIfReadOnly()
		{
			if (_readOnly)
			{
				throw new InvalidOperationException(@"Context is in read-only state, saving is not allowed");
			}
		}
	}
}