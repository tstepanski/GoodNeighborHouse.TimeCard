using GoodNeighborHouse.TimeCard.General;
using OrganizationiModel = GoodNeighborHouse.TimeCard.Web.Models.Organization;
using OrganizationEntity = GoodNeighborHouse.TimeCard.Data.Entities.Organization;

namespace GoodNeighborHouse.TimeCard.Web.Converters
{
    internal sealed class OrganizationConverter : IConverter<OrganizationEntity, OrganizationiModel>,
        IMapper<OrganizationiModel, OrganizationEntity>
    {
        public OrganizationiModel Convert(OrganizationEntity entity)
        {
            return new OrganizationiModel
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public void MapTo(OrganizationiModel businessObject, OrganizationEntity entity)
        {
            entity.Name = businessObject.Name;
        }
    }
}
