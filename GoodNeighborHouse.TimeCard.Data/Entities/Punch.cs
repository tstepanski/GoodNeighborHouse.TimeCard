using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodNeighborHouse.TimeCard.Data.Entities
{
	[Table(@"Punches")]
	public class Punch : AbstractIdentifiable
	{
		[Column(@"VolunteerId"), Required] public Guid VolunteerId { get; set; }

		[Column(@"DepartmentId"), Required] public Guid DepartmentId { get; set; }

		[Column(@"IsClockIn", TypeName = @"BIT"), Required]
		public bool IsClockIn { get; set; }

		[Column(@"PunchTime", TypeName = @"DATETIME"), Required]
		public DateTime PunchTime { get; set; }

		[Column(@"IsDeleted", TypeName = @"BIT"), Required]
		public bool IsDeleted { get; set; }

		[Column(@"CreatedBy", TypeName = @"STRING"), Required]
		[StringLength(100, MinimumLength = 1,
			ErrorMessage = "The CreatedBy value must be between 1 and 100 characters and is either the Id of the volunteer, or user")] 
		public String CreatedBy { get; set; }

		[Column(@"CreatedAt", TypeName = @"DATETIME"), Required]
		public DateTime CreatedAt { get; set; }

		[Column(@"LastUpdatedBy", TypeName = @"STRING"), Required] 
		[StringLength(100, MinimumLength = 1,
			ErrorMessage = "The LastUpdatedBy value must be between 1 and 100 characters and is either the Id of the volunteer, or user")] 
		public String LastUpdatedBy { get; set; }

		[Column(@"UpdatedAt", TypeName = @"DATETIME"), Required]
		public DateTime UpdatedAt { get; set; }

		[Column(@"Quantity")]
        public int Quantity { get; set; }

		[ForeignKey(nameof(VolunteerId))] public virtual Volunteer Volunteer { get; set; }

		[ForeignKey(nameof(DepartmentId))] public virtual Department Department { get; set; }
	}
}