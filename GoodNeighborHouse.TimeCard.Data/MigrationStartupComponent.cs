using System.Threading;
using System.Threading.Tasks;
using GoodNeighborHouse.TimeCard.Data.Context;
using GoodNeighborHouse.TimeCard.General;
using Microsoft.EntityFrameworkCore;

namespace GoodNeighborHouse.TimeCard.Data
{
	internal sealed class MigrationStartupComponent : IStartupComponent
	{
		private readonly IGNHContextFactory _dbContextFactory;

		public MigrationStartupComponent(IGNHContextFactory dbContextFactory)
		{
			_dbContextFactory = dbContextFactory;
		}

		public async Task Run(CancellationToken cancellationToken = default)
		{
			await using (var dbContext = _dbContextFactory.Create(false))
			{
				await dbContext.Database.MigrateAsync(cancellationToken);
			}
		}
	}
}