using System;

namespace GoodNeighborHouse.TimeCard.Web.Models
{
    public class OrganizationYTDReport
    {
        public Guid OrgId { get; set; }
        public decimal HoursWorked { get; set; }
        public int NumberofVolunteers { get; set; }
        public string DepartmentName { get; set; }
        public int Month { get; set; }

    }
}
