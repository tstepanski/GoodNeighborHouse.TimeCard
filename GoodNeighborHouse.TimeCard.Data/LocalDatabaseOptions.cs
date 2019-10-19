using System.IO;
using Microsoft.EntityFrameworkCore;

namespace GoodNeighborHouse.TimeCard.Data
{
	internal sealed class LocalDatabaseOptions : IDatabaseOptions
	{
		private LocalDatabaseOptions()
		{
			var connectionString = File.ReadAllText(@"LOCAL_CONNECTION_STRING");
			var dbContextOptionsBuilder = new DbContextOptionsBuilder();
			
			Options = dbContextOptionsBuilder
				.UseSqlServer(connectionString)
				.Options;
		}
		
		public DbContextOptions Options { get; }
		
		public static IDatabaseOptions Instance { get; } = new LocalDatabaseOptions();
	}
}