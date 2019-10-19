using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodNeighborHouse.TimeCard.Data.Entities
{
    [Table("DepartmentVolunteers")]
    public class DepartmentVolunteer : AbstractIdentifiable
    {
        [Required]
        [Column("DepartmentId")]
        public Guid DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [Required]
        [Column("VolunteerId")]
        public Guid VolunteerId { get; set; }
        public virtual Volunteer Volunteer { get; set; }
    }
}
