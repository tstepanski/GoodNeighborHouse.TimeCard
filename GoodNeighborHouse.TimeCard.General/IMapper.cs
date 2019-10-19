namespace GoodNeighborHouse.TimeCard.General
{
	public interface IMapper<in TFrom, in TTo>
	{
		void MapTo(TFrom from, TTo to);
	}
}