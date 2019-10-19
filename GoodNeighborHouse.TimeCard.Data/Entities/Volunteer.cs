using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodNeighborHouse.TimeCard.Data.Entities
{
	[Table("Volunteers")]
	public class Volunteer : AbstractIdentifiable
	{
		private const int MinimumAgeInYears = 2;

		[Column("FirstName", TypeName = "nvarchar(100)")]
		[StringLength(100, MinimumLength = 1,
			ErrorMessage = "The FirstName value must be between 1 and 100 characters. ")]
		public string FirstName { get; set; }

		[Required]
		[Column("LastName", TypeName = "nvarchar(100)")]
		[StringLength(100, MinimumLength = 1,
			ErrorMessage = "The LastName value must be between 1 and 100 characters. ")]
		public string LastName { get; set; }

        [Column(@"OrganizationId")] 
        public Guid? OrganizationId { get; set; }

        [Required]
		[Column("Username", TypeName = "nvarchar(250)")]
		[StringLength(6, ErrorMessage = "The Username value cannot exceed 250 characters. ")]
		public string Username { get; set; }

        [Required]
        [Column("IsPaid", TypeName = "bit")]
        public bool IsPaid { get; set; }

        [Required]
        [Column("IsGroup", TypeName = "bit")]
        public bool IsGroup { get; set; }

        public virtual ICollection<DepartmentVolunteer> DepartmentVolunteers { get; set; }

	}
}