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

		[Column(@"CreatedBy"), Required] public Guid CreatedBy { get; set; }

		[Column(@"CreatedAt", TypeName = @"DATETIME"), Required]
		public DateTime CreatedAt { get; set; }

		[Column(@"LastUpdatedBy"), Required] public Guid LastUpdatedBy { get; set; }

		[Column(@"UpdatedAt", TypeName = @"DATETIME"), Required]
		public DateTime UpdatedAt { get; set; }

		[ForeignKey(nameof(VolunteerId))] public Volunteer Volunteer { get; set; }

		[ForeignKey(nameof(DepartmentId))] public Department Department { get; set; }
	}
}