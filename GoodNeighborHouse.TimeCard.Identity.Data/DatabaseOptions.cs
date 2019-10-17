using Microsoft.EntityFrameworkCore;

namespace GoodNeighborHouse.TimeCard.Identity.Data
{
	internal sealed class DatabaseOptions : IDatabaseOptions
	{
		public DatabaseOptions(DbContextOptions options)
		{
			Options = options;
		}

		public DbContextOptions Options { get; }
	}
}