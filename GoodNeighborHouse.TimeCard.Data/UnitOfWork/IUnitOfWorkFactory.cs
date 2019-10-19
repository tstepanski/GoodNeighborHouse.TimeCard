namespace GoodNeighborHouse.TimeCard.Data.UnitOfWork
{
	public interface IUnitOfWorkFactory
	{
		IUnitOfWork CreateReadOnly();

		IUnitOfWork CreateReadWrite();
	}
}