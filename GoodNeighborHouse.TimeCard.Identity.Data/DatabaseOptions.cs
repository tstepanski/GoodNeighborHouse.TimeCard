using Microsoft.EntityFrameworkCore;

namespace GoodNeighborHouse.TimeCard.Identity.Data
{
	internal sealed class DatabaseOptions : IDatabaseOptions
	{
		public DatabaseOptions(DbContextOptions<IdentityContext> options)
		{
			Options = options;
		}

		public DbContextOptions<IdentityContext> Options { get; }
	}
}