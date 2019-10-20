using System;
using System.Collections.Immutable;
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
		private readonly IDatabaseSet<Department> _departmentsWrapper;
		private readonly IDatabaseSet<Organization> _organizationsWrapper;
		private readonly IDatabaseSet<Punch> _punchesWrapper;
		private readonly IDatabaseSet<Volunteer> _volunteersWrapper;
		private readonly IDatabaseSet<Reconciliation> _reconciliationsWrapper;
		private readonly IDatabaseSet<DepartmentVolunteer> _departmentVolunteersWrapper;

		public GNHContext(DbContextOptions options, bool readOnly = false) : base(options)
		{
			_readOnly = readOnly;
			_departmentsWrapper = DatabaseSetWrapper.CreateFrom(() => Departments);
			_organizationsWrapper = DatabaseSetWrapper.CreateFrom(() => Organizations);
			_punchesWrapper = DatabaseSetWrapper.CreateFrom(() => Punches);
			_volunteersWrapper = DatabaseSetWrapper.CreateFrom(() => Volunteers);
			_reconciliationsWrapper = DatabaseSetWrapper.CreateFrom(() => Reconciliations);
			_departmentVolunteersWrapper = DatabaseSetWrapper.CreateFrom(() => DepartmentVolunteers);
		}

#if DEBUG
		public GNHContext() : this(LocalDatabaseOptions.Instance.Options)
		{
		}
#endif

		public DbSet<Department> Departments { get; set; }
		public DbSet<Organization> Organizations { get; set; }
		public DbSet<Punch> Punches { get; set; }
		public DbSet<Volunteer> Volunteers { get; set; }
		public DbSet<Reconciliation> Reconciliations { get; set; }
		public DbSet<DepartmentVolunteer> DepartmentVolunteers { get; set; }


		IDatabaseSet<Department> IGNHContext.Departments => _departmentsWrapper;
		IDatabaseSet<Organization> IGNHContext.Organizations => _organizationsWrapper;
		IDatabaseSet<Punch> IGNHContext.Punches => _punchesWrapper;
		IDatabaseSet<Volunteer> IGNHContext.Volunteers => _volunteersWrapper;
		IDatabaseSet<Reconciliation> IGNHContext.Reconciliations => _reconciliationsWrapper;
		IDatabaseSet<DepartmentVolunteer> IGNHContext.DepartmentVolunteers => _departmentVolunteersWrapper;

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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			SetUpSequentialIds(modelBuilder);
			SeedDepartments(modelBuilder);

			var foreignKeys = modelBuilder
				.Model
				.GetEntityTypes()
				.Where(type => type.ClrType != typeof(DepartmentVolunteer))
				.SelectMany(type => type.GetForeignKeys());

			foreach (var foreignKey in foreignKeys)
			{
				foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
			}
		}

		private static void SetUpSequentialIds(ModelBuilder modelBuilder)
		{
			var idProperties = modelBuilder
				.Model
				.GetEntityTypes()
				.Where(entityType => typeof(AbstractIdentifiable).IsAssignableFrom(entityType.ClrType))
				.Select(entityType => entityType.FindProperty(nameof(AbstractIdentifiable.Id)));

			foreach (var property in idProperties)
			{
				property.SetDefaultValueSql(@"NEWSEQUENTIALID()");
			}
		}

		private static void SeedDepartments(ModelBuilder modelBuilder)
		{
			var dentalDepartment = new Department
			{
				Id = new Guid("B4240B56-6FF2-E911-9AE8-D0C637A95AE1"),
				Name = "Dental"
			};

			var humanServicesDepartment = new Department
			{
				Id = new Guid("B5240B56-6FF2-E911-9AE8-D0C637A95AE1"),
				Name = "Human Services"
			};

			var medicalDepartment = new Department
			{
				Id = new Guid("B6240B56-6FF2-E911-9AE8-D0C637A95AE1"),
				Name = "Medical"
			};

			var seedDepartments = ImmutableArray.Create(
				dentalDepartment,
				humanServicesDepartment,
				medicalDepartment);

			modelBuilder
				.Entity<Department>()
				.HasData(seedDepartments);
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