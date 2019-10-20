using System;
using System.Collections.Generic;
using System.Linq;
using GoodNeighborHouse.TimeCard.Data.Context;
using GoodNeighborHouse.TimeCard.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodNeighborHouse.TimeCard.Data.Repositories
{
	internal sealed class ReportingRepository : IReportingRepository
	{
		private static readonly decimal MillisecondsPerHour = (decimal) TimeSpan.FromHours(1).TotalMilliseconds;
		private readonly IGNHContext _context;

		public ReportingRepository(IGNHContext context)
		{
			_context = context;
		}

		public IAsyncEnumerable<VolunteerDailyReport> GetAllForPeriodAsync(DateTime start, DateTime end)
		{
			return _context
				.Reconciliations
				.Where(reconciliation => reconciliation.PunchIn.PunchTime >= start &&
				                         reconciliation.PunchOut.PunchTime <= end)
				.GroupBy(reconciliation => new
				{
					reconciliation.PunchIn.PunchTime.Date,
					reconciliation.PunchIn.Volunteer
				})
				.Select(reconciliations => new
				{
					reconciliations.Key.Volunteer,
					FirstPunch = reconciliations
						.OrderBy(reconciliation => reconciliation.PunchIn.PunchTime)
						.First()
						.PunchIn,
					LastPunch = reconciliations
						.OrderBy(reconciliation => reconciliation.PunchOut.PunchTime)
						.Last()
						.PunchOut,
					TotalHoursWorked = reconciliations
						                   .Sum(reconciliation => reconciliation.Difference) / MillisecondsPerHour
				})
				.Select(volunteerDayReport => new VolunteerDailyReport
				{
					ClockIn = volunteerDayReport.FirstPunch.PunchTime,
					ClockOut = volunteerDayReport.LastPunch.PunchTime,
					DepartmentName = volunteerDayReport.FirstPunch.Department.Name,
					FirstName = volunteerDayReport.Volunteer.FirstName,
					LastName = volunteerDayReport.Volunteer.LastName,
					HoursWorked = volunteerDayReport.TotalHoursWorked,
					OrganizationName = string.Empty, //TODO: Add org name
					PaidVolunteer = volunteerDayReport.Volunteer.IsPaid,
					NumberInGroup = volunteerDayReport.FirstPunch.Quantity
				})
				.OrderBy(report => report.ClockIn.Date)
				.ThenBy(report => report.LastName)
				.ThenBy(report => report.FirstName)
				.ThenBy(report => report.DepartmentName)
				.AsAsyncEnumerable();
		}
	}
}