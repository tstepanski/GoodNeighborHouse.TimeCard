using GoodNeighborHouse.TimeCard.General;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoodNeighborHouse.TimeCard.Identity.Data
{
    public sealed class IdentityDataRegistrar : IRegistrar
    {
        public IRegistrationContext PerformRegistrations(IRegistrationContext registrationContext)
        {
            registrationContext
                .RegisterSingleton<IIdentityContextFactory, IdentityContextFactory>()
                .RegisterSingleton<IStartupComponent, MigrationStartupComponent>()
                .RegisterSingleton<IDatabaseOptions, DatabaseOptions>(() =>
                    new DatabaseOptions(RunBuilder(registrationContext, new DbContextOptionsBuilder<IdentityContext>()).Options))
                .Services
                .AddDbContext<IdentityContext>(options => RunBuilder(registrationContext, (DbContextOptionsBuilder<IdentityContext>)options))
                .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<IdentityContext>();

            /*
             * services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores();
    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o => o.LoginPath = new PathString("/login"))
                .AddFacebook(o =>
                {
                    o.AppId = Configuration["facebook:appid"];
                    o.AppSecret = Configuration["facebook:appsecret"];
                });
                */

			return registrationContext;
		}

		private static DbContextOptionsBuilder<IdentityContext> RunBuilder(IRegistrationContext registrationContext,
			DbContextOptionsBuilder<IdentityContext> builder)
		{
			return builder.UseSqlServer(registrationContext.Configuration.GetConnectionString(@"IdentityConnection"));
		}
	}
}
 