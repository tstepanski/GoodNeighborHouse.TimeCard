using System;
using System.Threading;
using System.Threading.Tasks;
using GoodNeighborHouse.TimeCard.Data.Context;
using GoodNeighborHouse.TimeCard.Data.Entities;
using GoodNeighborHouse.TimeCard.Data.General;
using Microsoft.EntityFrameworkCore;

namespace GoodNeighborHouse.TimeCard.Data.Repositories
{
	internal sealed class VolunteerRepository : AbstractRepository<Volunteer, Guid>, IVolunteerRepository
	{
		public VolunteerRepository(IGNHContext context) : base(context)
		{
		}

		protected override IDatabaseSet<Volunteer> DbSet => Context.Volunteers;

		public Task<Volunteer> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
		{
			return DbSet
				.SingleOrDefaultAsync(volunteer => volunteer.Username == userName, cancellationToken);
		}
	}
}