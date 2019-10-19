using System;
using GoodNeighborHouse.TimeCard.Data.Entities;

namespace GoodNeighborHouse.TimeCard.Data.Repositories
{
    public interface IDepartmentRepository : IFullRepository<Department, Guid>
    {
    }
}
