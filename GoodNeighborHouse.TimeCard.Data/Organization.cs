using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodNeighborHouse.TimeCard.Data
{
    public class Organization : AbstractIdentifiable
    {
        [Required]
        [Column("Name", TypeName = "nvarchar(100)")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The organization name must be between 1 and 100 characters. ")]
        public string Name { get; set; }
    }
}
