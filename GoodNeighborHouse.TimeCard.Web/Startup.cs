using GoodNeighborHouse.TimeCard.Data;
using GoodNeighborHouse.TimeCard.General;
using GoodNeighborHouse.TimeCard.Identity.Data;
using GoodNeighborHouse.TimeCard.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using GoodNeighborHouse.TimeCard.Web.Converters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DepartmentModel = GoodNeighborHouse.TimeCard.Web.Models.Department;
using DepartmentEntity = GoodNeighborHouse.TimeCard.Data.Entities.Department;
using OrganizationModel = GoodNeighborHouse.TimeCard.Web.Models.Organization;
using OrganizationEntity = GoodNeighborHouse.TimeCard.Data.Entities.Organization;
using VolunteerModel = GoodNeighborHouse.TimeCard.Web.Models.Volunteer;
using VolunteerEntity = GoodNeighborHouse.TimeCard.Data.Entities.Volunteer;

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
                .RegisterSingleton<IConverter<DepartmentEntity, DepartmentModel>, DepartmentConverter>()
                .RegisterSingleton<IMapper<DepartmentModel, DepartmentEntity>, DepartmentConverter>()
                .RegisterSingleton<IConverter<OrganizationEntity, OrganizationModel>, OrganizationConverter>()
                .RegisterSingleton<IMapper<OrganizationModel, OrganizationEntity>, OrganizationConverter>()
                .RegisterSingleton<IConverter<VolunteerEntity, VolunteerModel>, VolunteerConverter>()
                .RegisterSingleton<IMapper<VolunteerModel, VolunteerEntity>, VolunteerConverter>()
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
                .AddCookie(o => o.LoginPath = new PathString("/login/login"));

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