using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodNeighborHouse.TimeCard.Data
{
    [Table("Volunteers")]
    public class Volunteer : AbstractIdentifiable, IValidatableObject
    {
        [Required]
        [Column("FirstName", TypeName = "nvarchar(100)")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The FirstName value must be between 1 and 100 characters. ")]
        public string FirstName { get; set; }

        [Required]
        [Column("LastName", TypeName = "nvarchar(100)")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The LastName value must be between 1 and 100 characters. ")]
        public string LastName { get; set; }

        [Required]
        [Column("DOB", TypeName = "datetime")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        [Column("PIN", TypeName = "nvarchar(6)")]
        [StringLength(6, ErrorMessage = "The PIN value cannot exceed 6 characters. ")]
        public string PIN { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            int minAge = 12;
            if (DOB > DateTime.Now.AddYears(-minAge))
            {
                results.Add(new ValidationResult("Volunteers must be at least " + minAge + " years old.", new[] { "StartDateTime" }));
            }
            return results;
        }
    }
}
