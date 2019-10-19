using Microsoft.EntityFrameworkCore;

namespace GoodNeighborHouse.TimeCard.Data.General
{
	internal interface IDatabaseOptions
	{
		DbContextOptions Options { get; }
	}
}