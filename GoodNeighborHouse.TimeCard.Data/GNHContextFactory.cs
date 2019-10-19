namespace GoodNeighborHouse.TimeCard.Data
{
	internal sealed class GNHContextFactory : IGNHContextFactory
	{
		private readonly IDatabaseOptions _databaseOptions;

		public GNHContextFactory(IDatabaseOptions databaseOptions)
		{
			_databaseOptions = databaseOptions;
		}

		public GNHContext Create()
		{
			return new GNHContext(_databaseOptions.Options);
		}
	}
}