using System;

namespace GoodNeighborHouse.TimeCard.Web.Models
{
	public class Punch : IPunch
	{
		public Guid Id { get; set; }
		public Guid VolunteerId { get; set; }

		public bool IsClockIn { get; set; }

		public DateTime PunchTime { get; set; }
		public bool IsDeleted { get; set; }

		public string CreatedBy { get; set; }

		public DateTime CreatedAt { get; set; }

		public string LastUpdatedBy { get; set; }

		public DateTime UpdatedAt { get; set; }

		public DateTime GetDate()
		{
			return PunchTime.Date;
		}
	}
}