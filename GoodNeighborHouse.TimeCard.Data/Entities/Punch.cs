using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodNeighborHouse.TimeCard.Data.Entities
{
    [Table(@"Punch")]
    public class Punch {
        [Key, Column(@"ID", TypeName = @"INT"), Required]
        public int Id { get; set; }

        [Column(@"VolunteerId", TypeName = @"INT"), Required]
        public int VolunteerId { get; set; }
    
        [Column(@"PunchTime", TypeName = @"DATETIME"), Required]
        public DateTime PunchTime { get; set; }
    
        [Column(@"IsDeleted", TypeName = @"BIT"), Required]
        public bool IsDeleted { get; set; }
    
        [Column(@"CreatedBy", TypeName = @"INT"), Required]
        public int CreatedBy { get; set; }
    
        [Column(@"CreatedAt", TypeName = @"DATETIME"), Required]
        public DateTime CreatedAt { get; set; }
    
        [Column(@"LastUpdatedBy", TypeName = @"INT"), Required]
        public int LastUpdatedBy { get; set; }
    
        [Column(@"UpdatedAt", TypeName = @"DATETIME"), Required]
        public DateTime UpdatedAt { get; set; }
    }
}