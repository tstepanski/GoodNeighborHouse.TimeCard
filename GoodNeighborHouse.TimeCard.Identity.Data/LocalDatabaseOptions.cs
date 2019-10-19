using System.IO;
using Microsoft.EntityFrameworkCore;

namespace GoodNeighborHouse.TimeCard.Identity.Data
{
	internal sealed class LocalDatabaseOptions : IDatabaseOptions
	{
		private LocalDatabaseOptions()
		{
			var connectionString = File.ReadAllText(@"LOCAL_CONNECTION_STRING");
			var dbContextOptionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
			
			Options = dbContextOptionsBuilder
				.UseSqlServer(connectionString)
				.Options;
		}
		
		public DbContextOptions<IdentityContext> Options { get; }
		
		public static IDatabaseOptions Instance { get; } = new LocalDatabaseOptions();
	}
}