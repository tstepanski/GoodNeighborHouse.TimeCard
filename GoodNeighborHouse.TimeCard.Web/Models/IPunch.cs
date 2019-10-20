using System;

namespace GoodNeighborHouse.TimeCard.Web.Models
{
	public interface IPunch
	{
		Guid VolunteerId { get; set; }
		bool IsClockIn { get; set; }
		DateTime GetDate();
	}
}