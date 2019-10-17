using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoodNeighborHouse.TimeCard.General
{
	public interface IRegistrationContext
	{
		IServiceCollection Services { get; }
		IConfiguration Configuration { get; }

		IRegistrationContext RegisterSingleton<TService, TImplementation>()
			where TService : class
			where TImplementation : class, TService;

		IRegistrationContext RegisterSingleton<TService, TImplementation>(Func<TImplementation> factory)
			where TService : class
			where TImplementation : class, TService;

		IRegistrationContext RegisterScoped<TService, TImplementation>()
			where TService : class
			where TImplementation : class, TService;
	}
}