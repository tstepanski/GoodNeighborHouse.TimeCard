using GoodNeighborHouse.TimeCard.Data;
using GoodNeighborHouse.TimeCard.General;
using GoodNeighborHouse.TimeCard.Identity.Data;
using GoodNeighborHouse.TimeCard.Web.Converters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
			RegistrationContext
				.New(services, Configuration)
				.Register<IdentityDataRegistrar>()
				.Register<GNHDataRegistrar>()
				.RegisterSingleton<IConverter<VolunteerEntity, VolunteerModel>, VolunteerConverter>()
				.RegisterSingleton<IMapper<VolunteerModel, VolunteerEntity>, VolunteerConverter>()
				.Complete()
				.AddHostedService<StartupServices>()
				.AddControllersWithViews();

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