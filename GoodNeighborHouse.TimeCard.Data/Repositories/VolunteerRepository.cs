using System;
using GoodNeighborHouse.TimeCard.Data.Context;
using GoodNeighborHouse.TimeCard.Data.Entities;
using GoodNeighborHouse.TimeCard.Data.General;

namespace GoodNeighborHouse.TimeCard.Data.Repositories
{
	internal sealed class VolunteerRepository : AbstractRepository<Volunteer, Guid>, IVolunteerRepository
	{
		public VolunteerRepository(IGNHContext context) : base(context)
		{
		}

		protected override IDatabaseSet<Volunteer> DbSet => Context.Volunteers;
	}
}