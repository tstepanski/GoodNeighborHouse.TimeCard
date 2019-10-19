using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace GoodNeighborHouse.TimeCard.General
{
	public static class LinqExtensions
	{
		public static async Task<IImmutableList<T>> ToImmutableArrayAsync<T>(this IAsyncEnumerable<T> values,
			CancellationToken cancellationToken = default)
		{
			var linkedList = new LinkedList<T>();

			await foreach (var value in values.WithCancellation(cancellationToken))
			{
				linkedList.AddFirst(value);
			}

			return linkedList.ToImmutableArray();
		}
	}
}