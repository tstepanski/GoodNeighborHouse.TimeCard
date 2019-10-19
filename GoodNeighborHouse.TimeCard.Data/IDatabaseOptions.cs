using Microsoft.EntityFrameworkCore;

namespace GoodNeighborHouse.TimeCard.Data
{
	internal interface IDatabaseOptions
	{
		DbContextOptions Options { get; }
	}
}