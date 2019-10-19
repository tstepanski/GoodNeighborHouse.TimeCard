using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodNeighborHouse.TimeCard.Data
{
	[Table("Volunteers")]
	public class Volunteer : AbstractIdentifiable, IValidatableObject
	{
		private const int MinimumAgeInYears = 12;

		[Required]
		[Column("FirstName", TypeName = "nvarchar(100)")]
		[StringLength(100, MinimumLength = 1,
			ErrorMessage = "The FirstName value must be between 1 and 100 characters. ")]
		public string FirstName { get; set; }

		[Required]
		[Column("LastName", TypeName = "nvarchar(100)")]
		[StringLength(100, MinimumLength = 1,
			ErrorMessage = "The LastName value must be between 1 and 100 characters. ")]
		public string LastName { get; set; }

		[Required]
		[Column("DOB", TypeName = "datetime")]
		[DataType(DataType.Date)]
		public DateTime DateOfBirth { get; set; }

		[Required]
		[Column("PIN", TypeName = "nvarchar(6)")]
		[StringLength(6, ErrorMessage = "The PIN value cannot exceed 6 characters. ")]
		public string Pin { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (DateOfBirth <= DateTime.Now.AddYears(-MinimumAgeInYears))
			{
				yield break;
			}

			var message = $@"Volunteers must be at least {MinimumAgeInYears} years old.";

			var memberNames = new[]
			{
				nameof(DateOfBirth)
			};

			yield return new ValidationResult(message, memberNames);
		}
	}
}