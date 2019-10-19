using System;
using System.Collections.Generic;

namespace GoodNeighborHouse.TimeCard.Web.Models
{
    public sealed class Volunteer
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Username { get; set; }
        public bool IsPaid { get; set; }
        public bool IsGroup { get; set; }
        public Guid? SelectedOrganizationId { get; set; }
        public List<Selection<Guid>> Departments { get; set; }
        public List<Organization> Organizations { get; set; }
	}
}