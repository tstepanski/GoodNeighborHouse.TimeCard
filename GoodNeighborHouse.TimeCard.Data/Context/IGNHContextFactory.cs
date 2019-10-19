namespace GoodNeighborHouse.TimeCard.Data.Context
{
	internal interface IGNHContextFactory
	{
		GNHContext Create(bool readOnly);
	}
}