using Microsoft.EntityFrameworkCore;

namespace GoodNeighborHouse.TimeCard.Identity.Data
{
	internal interface IDatabaseOptions
	{
		DbContextOptions<IdentityContext> Options { get; }
	}
}