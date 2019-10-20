using System;

namespace GoodNeighborHouse.TimeCard.Web.Models
{
	public sealed class MissingPunch : IPunch
	{
		public Guid VolunteerId { get; set; }
		public bool IsClockIn { get; set; }
		public DateTime ShouldBeBefore { get; set; }

		public DateTime GetDate()
		{
			return ShouldBeBefore.Date;
		}
	}
}