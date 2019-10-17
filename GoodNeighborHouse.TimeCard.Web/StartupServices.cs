using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNeighborHouse.TimeCard.General;
using Microsoft.Extensions.Hosting;

namespace GoodNeighborHouse.TimeCard.Web
{
	internal sealed class StartupServices : IHostedService
	{
		private readonly IReadOnlyList<IStartupComponent> _startupComponents;

		public StartupServices(IEnumerable<IStartupComponent> startupComponents)
		{
			_startupComponents = startupComponents.ToImmutableArray();
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			var startupTasks = _startupComponents.Select(component => component.Run(cancellationToken));

			return Task.WhenAll(startupTasks);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}