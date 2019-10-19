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
                .RegisterSingleton<IStartupComponent, MigrationStartupComponent>()
                .RegisterSingleton<IDatabaseOptions, DatabaseOptions>(() =>
                    new DatabaseOptions(RunBuilder(registrationContext, new DbContextOptionsBuilder<GNHContext>()).Options))
                .Services
                .AddDbContext<GNHContext>(options => RunBuilder(registrationContext, (DbContextOptionsBuilder<GNHContext>) options));
			return registrationContext;
		}

		private static DbContextOptionsBuilder<GNHContext> RunBuilder(IRegistrationContext registrationContext,
			DbContextOptionsBuilder<GNHContext> builder)
		{
			return builder.UseSqlServer(registrationContext.Configuration.GetConnectionString(@"DataConnection"));
		}
	}
}