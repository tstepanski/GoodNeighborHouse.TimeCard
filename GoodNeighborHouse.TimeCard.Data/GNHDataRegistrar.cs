using GoodNeighborHouse.TimeCard.Data.Context;
using GoodNeighborHouse.TimeCard.Data.General;
using GoodNeighborHouse.TimeCard.Data.Repositories.Factories;
using GoodNeighborHouse.TimeCard.Data.UnitOfWork;
using GoodNeighborHouse.TimeCard.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoodNeighborHouse.TimeCard.Data
{
	public sealed class GNHDataRegistrar : IRegistrar
	{
		public IRegistrationContext PerformRegistrations(IRegistrationContext registrationContext)
		{
			registrationContext
				.RegisterSingleton<IGNHContextFactory, GNHContextFactory>()
				.RegisterSingleton<IRepositoryFactory, VolunteerRepositoryFactory>()
				.RegisterSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>()
				.RegisterSingleton<IStartupComponent, MigrationStartupComponent>()
				.RegisterSingleton<IDatabaseOptions, DatabaseOptions>(() =>
					new DatabaseOptions(RunBuilder(registrationContext, new DbContextOptionsBuilder<GNHContext>())
						.Options))
				.Services
				.AddDbContext<GNHContext>(options =>
					RunBuilder(registrationContext, (DbContextOptionsBuilder<GNHContext>) options));
			return registrationContext;
		}

		private static DbContextOptionsBuilder<GNHContext> RunBuilder(IRegistrationContext registrationContext,
			DbContextOptionsBuilder<GNHContext> builder)
		{
			return builder
				.EnableSensitiveDataLogging()
				.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
				.UseLazyLoadingProxies()
				.UseSqlServer(registrationContext.Configuration.GetConnectionString(@"DataConnection"));
		}
	}
}