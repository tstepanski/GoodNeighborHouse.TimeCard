using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GoodNeighborHouse.TimeCard.Data
{
    public class GNHContext : DbContext
	{
        public DbSet<Volunteer> Volunteers { get; set; }

		public GNHContext(DbContextOptions options) : base(options)
		{
        }
		
#if DEBUG
		public GNHContext() : this(LocalDatabaseOptions.Instance.Options)
		{
			
		}
#endif

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var idProperties = modelBuilder
                .Model
                .GetEntityTypes()
                .Where(entityType => typeof(AbstractIdentifiable).IsAssignableFrom(entityType.ClrType))
                .Select(entityType => entityType.FindProperty(nameof(AbstractIdentifiable.ID)));

            foreach(var property in idProperties)
            {
                property.SetDefaultValueSql(@"NEWSEQUENTIALID()");
            }
        }
    }
}