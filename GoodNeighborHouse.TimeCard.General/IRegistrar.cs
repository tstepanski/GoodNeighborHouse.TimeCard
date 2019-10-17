namespace GoodNeighborHouse.TimeCard.General
{
	public interface IRegistrar
	{
		IRegistrationContext PerformRegistrations(IRegistrationContext registrationContext);
	}
}