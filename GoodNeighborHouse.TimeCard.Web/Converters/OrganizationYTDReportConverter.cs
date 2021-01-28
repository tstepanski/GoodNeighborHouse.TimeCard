using GoodNeighborHouse.TimeCard.General;
using OrganizationYTDReportEntity = GoodNeighborHouse.TimeCard.Data.Entities.OrganizationYTDReport;
using OrganizationYTDReportModel = GoodNeighborHouse.TimeCard.Web.Models.OrganizationYTDReport;

namespace GoodNeighborHouse.TimeCard.Web.Converters
{
    internal sealed class OrganizationYTDReportConverter : IConverter<OrganizationYTDReportEntity, OrganizationYTDReportModel>, IMapper<OrganizationYTDReportModel, OrganizationYTDReportEntity>
	{
		private readonly IConverter<OrganizationYTDReportEntity, OrganizationYTDReportModel> _organizationYTDReportConverter;

		public OrganizationYTDReportConverter(IConverter<OrganizationYTDReportEntity, OrganizationYTDReportModel> organizationYTDReportConverter)
		{
            _organizationYTDReportConverter = organizationYTDReportConverter;
		}

        public OrganizationYTDReportModel Convert(OrganizationYTDReportEntity entity)
        {
            return new OrganizationYTDReportModel()
            {
                DepartmentName = entity.DepartmentName,
                HoursWorked = entity.HoursWorked,
                Month = entity.Month,
                NumberofVolunteers = entity.NumberofVolunteers,
                OrgId = entity.OrgId
            };
        }

		public void MapTo(OrganizationYTDReportModel businessObject, OrganizationYTDReportEntity entity)
		{
            entity.DepartmentName = businessObject.DepartmentName;
            entity.HoursWorked = businessObject.HoursWorked;
            entity.Month = businessObject.Month;
            entity.NumberofVolunteers = businessObject.NumberofVolunteers;
            entity.OrgId = businessObject.OrgId;
        }
    }
}