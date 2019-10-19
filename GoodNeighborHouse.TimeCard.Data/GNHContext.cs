using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GoodNeighborHouse.TimeCard.Data
{
    public class GNHContext : DbContext
	{
        public DbSet<Department> Departments { get; set; }
        public DbSet<Organization> Organizations { get; set; }
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

            modelBuilder.Entity<Department>()
                .HasData(new List<Department>
                {
                    new Department() {ID = new Guid("00000000-0000-0000-0000-000000000001"), Name = "Dental"},
                    new Department() {ID = new Guid("00000000-0000-0000-0000-000000000002"), Name = "Human Services"},
                    new Department() {ID = new Guid("00000000-0000-0000-0000-000000000003"), Name = "Medical"}
                });
        }
    }
}