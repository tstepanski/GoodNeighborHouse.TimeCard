using System;

namespace GoodNeighborHouse.TimeCard.Data.Entities
{
	public class VolunteerDailyReport
	{
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public decimal HoursWorked { get; set; }
		public int NumberInGroup { get; set; }
		public bool PaidVolunteer { get; set; }
		public string DepartmentName { get; set; }
		public string OrganizationName { get; set; }
		public DateTime ClockIn { get; set; }
		public DateTime ClockOut { get; set; }
	}
}