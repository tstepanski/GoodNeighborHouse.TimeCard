using System;
using GoodNeighborHouse.TimeCard.Data.Context;
using GoodNeighborHouse.TimeCard.Data.Entities;
using GoodNeighborHouse.TimeCard.Data.General;

namespace GoodNeighborHouse.TimeCard.Data.Repositories
{
    internal sealed class DepartmentRepository : AbstractRepository<Department, Guid>, IDepartmentRepository
    {
        internal DepartmentRepository(IGNHContext context) : base(context)
        {
        }

        protected override IDatabaseSet<Department> DbSet => Context.Departments;
    }

   
}
