using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoodNeighborHouse.TimeCard.Identity.Data
{
	public class IdentityContext : IdentityDbContext
	{
		public IdentityContext(DbContextOptions options) : base(options)
		{
		}
		
#if DEBUG
		public IdentityContext() : this(LocalDatabaseOptions.Instance.Options)
		{
			
		}
#endif
	}
}