using System;
using System.Threading;
using System.Threading.Tasks;
using GoodNeighborHouse.TimeCard.Data.Entities;

namespace GoodNeighborHouse.TimeCard.Data.Repositories
{
	public interface IVolunteerRepository : IFullRepository<Volunteer, Guid>
	{
		Task<Volunteer> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
        Task<Volunteer> GetNewestVolunteerByName(string firstName, string lastName, CancellationToken cancellationToken = default);
	}
}