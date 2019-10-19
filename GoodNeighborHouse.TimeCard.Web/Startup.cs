using GoodNeighborHouse.TimeCard.Data;
using GoodNeighborHouse.TimeCard.General;
using GoodNeighborHouse.TimeCard.Identity.Data;
using GoodNeighborHouse.TimeCard.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GoodNeighborHouse.TimeCard.Web
{
    public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
            services = RegistrationContext
                .New(services, Configuration)
                .Register<IdentityDataRegistrar>()
                .Register<GNHDataRegistrar>()
                .Complete()
                .Configure<LdapConfig>(Configuration.GetSection("ldap"))
                .AddScoped<IAuthenticationService, LdapAuthenticationService>()
                .AddHostedService<StartupServices>();

            var isAdminUserPolicy = new AuthorizationPolicyBuilder()
                .RequireRole("Admin")
                .Build();

            services.AddMvc(options =>
            {
                options.Filters.Add(new ApplyPolicyOrAuthorizeFilter(isAdminUserPolicy));
            });

            services.AddControllersWithViews();

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o => o.LoginPath = new PathString("/login"));

			services.AddRazorPages();
		}

		public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment)
		{
			(webHostEnvironment.IsDevelopment()
					? applicationBuilder.UseDeveloperExceptionPage()
					: applicationBuilder
						.UseExceptionHandler("/Home/Error")
						.UseHsts())
				.UseHttpsRedirection()
                .UseStaticFiles()
				.UseRouting()
                .UseAuthentication()
				.UseAuthorization()
				.UseEndpoints(endpoints =>
				{
					endpoints.MapControllerRoute(@"default", @"{controller=Home}/{action=Index}/{id?}");
					endpoints.MapRazorPages();
				});
		}
	}
}