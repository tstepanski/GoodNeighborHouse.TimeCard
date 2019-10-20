using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GoodNeighborHouse.TimeCard.Data.Repositories;
using GoodNeighborHouse.TimeCard.Data.UnitOfWork;
using GoodNeighborHouse.TimeCard.General;
using Microsoft.AspNetCore.Mvc;

namespace GoodNeighborHouse.TimeCard.Web.Controllers
{
	public sealed class DataController : Controller
	{
		private readonly IUnitOfWorkFactory _unitOfWorkFactory;

		public DataController(IUnitOfWorkFactory unitOfWorkFactory)
		{
			_unitOfWorkFactory = unitOfWorkFactory;
		}

		[HttpGet(@"VolunteerByDay/{startTimeMillis}/{endTimeMillis}")]
		public async Task<IActionResult> GetReport([FromRoute] long startTimeMillis, [FromRoute] long endTimeMillis,
			CancellationToken cancellationToken = default)
		{
			var start = DateTimeOffset.FromUnixTimeMilliseconds(startTimeMillis).UtcDateTime;
			var end = DateTimeOffset.FromUnixTimeMilliseconds(endTimeMillis).UtcDateTime;

			using (var unitOfWork = _unitOfWorkFactory.CreateReadOnly())
			{
				var reportingRepository = unitOfWork.GetRepository<IReportingRepository>();

				await using (var memoryStream = new MemoryStream())
				{
					const string header =
						@"LastName,FirstName,HoursWorked,Date,Group,NumberOfPeopleInGroup,IsPaid,Department,In,Out";

					var bytes = Encoding.UTF8.GetBytes(header);

					memoryStream.Write(bytes);

					var allBytes = (await reportingRepository
							.GetAllForPeriodAsync(start, end)
							.ToImmutableArrayAsync(cancellationToken))
						.Select(report =>
							$@"""{report.LastName}"",""{report.FirstName}"",{report.HoursWorked},{report.ClockIn.Date:yyyy.MM.dd},""{report.OrganizationName}"",{report.NumberInGroup},{report.PaidVolunteer},""{report.DepartmentName}"",{report.ClockIn:hh:mm},{report.ClockOut:hh:mm}{Environment.NewLine}")
						.Select(Encoding.UTF8.GetBytes);

					foreach (var lineBytes in allBytes)
					{
						memoryStream.Write(lineBytes);
					}

					memoryStream.Position = 0;

					return File(memoryStream, @"application/octet-stream", @"Report.csv");
				}
			}
		}
	}
}