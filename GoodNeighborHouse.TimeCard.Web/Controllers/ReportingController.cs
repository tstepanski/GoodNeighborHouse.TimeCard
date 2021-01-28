using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNeighborHouse.TimeCard.Data.Entities;
using GoodNeighborHouse.TimeCard.Data.Repositories;
using GoodNeighborHouse.TimeCard.Data.UnitOfWork;
using GoodNeighborHouse.TimeCard.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReconciliationModel = GoodNeighborHouse.TimeCard.Web.Models.Reconciliation;
using ReconciliationEntity = GoodNeighborHouse.TimeCard.Data.Entities.Reconciliation;
using OrganizationModel = GoodNeighborHouse.TimeCard.Web.Models.Organization;
using OrganizationEntity = GoodNeighborHouse.TimeCard.Data.Entities.Organization;
using OrganizationYTDReportModel = GoodNeighborHouse.TimeCard.Web.Models.OrganizationYTDReport;
using OrganizationYTDReportEntity = GoodNeighborHouse.TimeCard.Data.Entities.OrganizationYTDReport;
using GoodNeighborHouse.TimeCard.Web.Models;
using System.Collections.Generic;

namespace GoodNeighborHouse.TimeCard.Web.Controllers
{
	[Authorize]
	public sealed class ReportingController : Controller
	{
		private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IConverter<OrganizationEntity, OrganizationModel> _organizationConverter;
        private readonly IConverter<OrganizationYTDReportEntity, OrganizationYTDReportModel> _organizationYTDReportConverter;

        public ReportingController(IUnitOfWorkFactory unitOfWorkFactory, IConverter<OrganizationEntity, OrganizationModel> organizationConverter, 
            IConverter<OrganizationYTDReportEntity, OrganizationYTDReportModel> organizationYTDReportConverter)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
            _organizationConverter = organizationConverter;
            _organizationYTDReportConverter = organizationYTDReportConverter;
        }

		[HttpGet]
		public async Task<IActionResult> Organizations(Guid? orgId, int year = 0, CancellationToken cancellationToken = default)
		{
            using (var unitOfWork = _unitOfWorkFactory.CreateReadOnly())
            {
                var organizations = (await unitOfWork
                        .GetRepository<IOrganizationRepository>()
                        .GetAllAsync()
                        .ToImmutableArrayAsync(cancellationToken))
                    .Select(_organizationConverter.Convert)
                    .ToList();

                if (organizations == null)
                {
                    return NotFound();
                }
                if (year == 0)
                    year = DateTime.Now.Year;

                OrganizationReport reportData = new OrganizationReport() { Organizations = organizations, Year = year};
                if (orgId.HasValue)
                {
                    reportData.OrganizationId = orgId;

                    var reportingRepository = unitOfWork.GetRepository<IReportingRepository>();
                    var entity = (await reportingRepository
                        .GetYTDForOrgAsync(orgId.Value, DateTime.Now.Year)
                        .ToImmutableArrayAsync(cancellationToken)).ToList();
                    var dataList = new List<OrganizationYTDReportModel>();
                    foreach (var item in entity)
                    {
                        dataList.Add(_organizationYTDReportConverter.Convert(item));
                        reportData.YTDHours += item.HoursWorked;
                        reportData.YTDVolunteers += item.NumberofVolunteers;
                    }

                    reportData.Data = dataList;
                }

                return View("Organizations", reportData);

            }

		}

	}
}