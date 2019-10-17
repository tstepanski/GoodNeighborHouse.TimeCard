using Microsoft.Extensions.DependencyInjection;

namespace GoodNeighborHouse.TimeCard.General
{
	public static class RegistrationContextExtensions
	{
		public static IRegistrationContext Register<TRegistrar>(this IRegistrationContext registrationContext)
			where TRegistrar : IRegistrar, new()
		{
			var registrar = new TRegistrar();

			return registrar.PerformRegistrations(registrationContext);
		}

		public static IServiceCollection Complete(this IRegistrationContext registrationContext)
		{
			return registrationContext.Services;
		}
	}
}