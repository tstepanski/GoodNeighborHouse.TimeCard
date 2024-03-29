using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodNeighborHouse.TimeCard.Data.Entities
{
	[Table(@"Punches")]
	public class Punch : AbstractIdentifiable, IValidatableObject
	{
		[Column(@"VolunteerId"), Required] public Guid VolunteerId { get; set; }

		[Column(@"DepartmentId"), Required] public Guid DepartmentId { get; set; }

		[Column(@"IsClockIn", TypeName = @"BIT"), Required]
		public bool IsClockIn { get; set; }

		[Column(@"PunchTime", TypeName = @"DATETIME"), Required]
		public DateTime PunchTime { get; set; }

		[Column(@"IsDeleted", TypeName = @"BIT"), Required]
		public bool IsDeleted { get; set; }

		[Column(@"CreatedAt", TypeName = @"DATETIME"), Required]
		public DateTime CreatedAt { get; set; }

		[Column(@"UpdatedAt", TypeName = @"DATETIME"), Required]
		public DateTime UpdatedAt { get; set; }

		[Column(@"Quantity")] public int Quantity { get; set; }

		[Column(@"LastUpdatedBy", TypeName = "varchar(500)"), Required]
		[StringLength(500, MinimumLength = 1,
			ErrorMessage =
				"The LastUpdatedBy value must be between 1 and 500 characters and is either the Id of the volunteer or user")]
		public string LastUpdatedBy { get; set; }

		[Column(@"CreatedBy", TypeName = "varchar(500)"), Required]
		[StringLength(500, MinimumLength = 1,
			ErrorMessage =
				"The CreatedBy value must be between 1 and 500 characters and is either the Id of the volunteer or user")]
		public string CreatedBy { get; set; }

		[ForeignKey(nameof(VolunteerId))] public virtual Volunteer Volunteer { get; set; }

		[ForeignKey(nameof(DepartmentId))] public virtual Department Department { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (Quantity >= 1)
			{
				if (Volunteer.IsGroup)
				{
				}
				else
				{
					if (Quantity == 1)
					{
					}
					else
					{
						yield return new ValidationResult("Only 1", new[] {nameof(Quantity)});
					}
				}
			}
			else
			{
				yield return new ValidationResult("At least 1", new[] {nameof(Quantity)});
			}
		}
	}
}