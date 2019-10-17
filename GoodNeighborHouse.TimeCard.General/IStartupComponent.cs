using System.Threading;
using System.Threading.Tasks;

namespace GoodNeighborHouse.TimeCard.General
{
	public interface IStartupComponent
	{
		Task Run(CancellationToken cancellationToken = default);
	}
}