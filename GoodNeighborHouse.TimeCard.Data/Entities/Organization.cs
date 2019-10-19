using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodNeighborHouse.TimeCard.Data.Entities
{
	public class Organization : AbstractIdentifiable, IValidatableObject
	{
		[Required]
		[Column("Name", TypeName = "nvarchar(100)")]
		[StringLength(100, MinimumLength = 1,
			ErrorMessage = "The organization name must be between 1 and 100 characters. ")]
		public string Name { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			List<ValidationResult> results = new List<ValidationResult>();
			Validator.TryValidateProperty(this.Name, new ValidationContext(this, null, null) {MemberName = "Name"},
				results);
			return results;
		}
	}
}