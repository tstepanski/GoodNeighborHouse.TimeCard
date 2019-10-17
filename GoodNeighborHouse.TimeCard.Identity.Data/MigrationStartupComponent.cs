using System.Threading;
using System.Threading.Tasks;
using GoodNeighborHouse.TimeCard.General;
using Microsoft.EntityFrameworkCore;

namespace GoodNeighborHouse.TimeCard.Identity.Data
{
	internal sealed class MigrationStartupComponent : IStartupComponent
	{
		private readonly IIdentityContextFactory _identityContextFactory;

		public MigrationStartupComponent(IIdentityContextFactory identityContextFactory)
		{
			_identityContextFactory = identityContextFactory;
		}

		public async Task Run(CancellationToken cancellationToken = default)
		{
			await using (var identityContext = _identityContextFactory.Create())
			{
				await identityContext.Database.MigrateAsync(cancellationToken);
			}
		}
	}
}