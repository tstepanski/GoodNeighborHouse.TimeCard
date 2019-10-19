using System;
using GoodNeighborHouse.TimeCard.Data.Context;
using GoodNeighborHouse.TimeCard.Data.Entities;
using GoodNeighborHouse.TimeCard.Data.General;

namespace GoodNeighborHouse.TimeCard.Data.Repositories
{
	internal sealed class DepartmentVolunteerRepository : AbstractRepository<DepartmentVolunteer, Guid>, IDepartmentVolunteerRepository
	{
		public DepartmentVolunteerRepository(IGNHContext context) : base(context)
		{
		}

		protected override IDatabaseSet<DepartmentVolunteer> DbSet => Context.DepartmentVolunteers;
	}
}