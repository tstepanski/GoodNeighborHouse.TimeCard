using GoodNeighborHouse.TimeCard.Data.General;

namespace GoodNeighborHouse.TimeCard.Data.Context
{
	internal sealed class GNHContextFactory : IGNHContextFactory
	{
		private readonly IDatabaseOptions _databaseOptions;

		public GNHContextFactory(IDatabaseOptions databaseOptions)
		{
			_databaseOptions = databaseOptions;
		}

		public GNHContext Create(bool readOnly)
		{
			return new GNHContext(_databaseOptions.Options, readOnly);
		}
	}
}