namespace GoodNeighborHouse.TimeCard.Identity.Data
{
	internal sealed class IdentityContextFactory : IIdentityContextFactory
	{
		private readonly IDatabaseOptions _databaseOptions;

		public IdentityContextFactory(IDatabaseOptions databaseOptions)
		{
			_databaseOptions = databaseOptions;
		}

		public IdentityContext Create()
		{
			return new IdentityContext(_databaseOptions.Options);
		}
	}
}