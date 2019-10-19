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
                .Select(entityType => entityType.FindProperty(nameof(AbstractIdentifiable.Id)));

            foreach(var property in idProperties)
            {
                property.SetDefaultValueSql(@"NEWSEQUENTIALID()");
            }

            modelBuilder.Entity<Department>()
                .HasData(new List<Department>
                {
                    new Department() {ID = new Guid("B4240B56-6FF2-E911-9AE8-D0C637A95AE1"), Name = "Dental"},
                    new Department() {ID = new Guid("B5240B56-6FF2-E911-9AE8-D0C637A95AE1"), Name = "Human Services"},
                    new Department() {ID = new Guid("B6240B56-6FF2-E911-9AE8-D0C637A95AE1"), Name = "Medical"}
                });
        }
    }
}