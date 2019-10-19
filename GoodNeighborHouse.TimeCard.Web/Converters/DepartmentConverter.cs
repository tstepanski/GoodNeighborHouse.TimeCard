using GoodNeighborHouse.TimeCard.General;
using DepartmentModel = GoodNeighborHouse.TimeCard.Web.Models.Department;
using DepartmentEntity = GoodNeighborHouse.TimeCard.Data.Entities.Department;

namespace GoodNeighborHouse.TimeCard.Web.Converters
{
    internal sealed class DepartmentConverter : IConverter<DepartmentEntity, DepartmentModel>,
        IMapper<DepartmentModel, DepartmentEntity>
    {
        public DepartmentModel Convert(DepartmentEntity entity)
        {
            return new DepartmentModel
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public void MapTo(DepartmentModel businessObject, DepartmentEntity entity)
        {
            entity.Name = businessObject.Name;
        }
    }
}
