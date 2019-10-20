using System;
using System.Collections.Generic;

namespace GoodNeighborHouse.TimeCard.Web.Models
{
	public sealed class OrganizationReport
	{
		public Guid? OrganizationId { get; set; }
		public List<Organization> Organizations { get; set; }
        public List<OrganizationYTDReport> Data { get; set; }
        public decimal YTDHours { get; set; }
        public int YTDVolunteers { get; set; }
        public int Year;
	}
}