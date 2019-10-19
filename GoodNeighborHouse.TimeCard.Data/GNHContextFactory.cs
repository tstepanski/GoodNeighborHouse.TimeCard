namespace GoodNeighborHouse.TimeCard.Data
{
	// ReSharper disable once InconsistentNaming
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