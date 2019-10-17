using System;
using GoodNeighborHouse.TimeCard.General;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoodNeighborHouse.TimeCard.Web
{
	internal sealed class RegistrationContext : IRegistrationContext
	{
		private RegistrationContext(IServiceCollection services, IConfiguration configuration)
		{
			Services = services;
			Configuration = configuration;
		}

		public IServiceCollection Services { get; }
		public IConfiguration Configuration { get; }

		public IRegistrationContext RegisterSingleton<TService, TImplementation>()
			where TService : class
			where TImplementation : class, TService
		{
			Services.AddSingleton<TService, TImplementation>();

			return this;
		}

		public IRegistrationContext RegisterSingleton<TService, TImplementation>(Func<TImplementation> factory)
			where TService : class
			where TImplementation : class, TService
		{
			Services.AddSingleton<TService, TImplementation>(_ => factory());

			return this;
		}

		public IRegistrationContext RegisterScoped<TService, TImplementation>()
			where TService : class
			where TImplementation : class, TService
		{
			Services.AddScoped<TService, TImplementation>();

			return this;
		}

		public static IRegistrationContext New(IServiceCollection services, IConfiguration configuration)
		{
			return new RegistrationContext(services, configuration);
		}
	}
}