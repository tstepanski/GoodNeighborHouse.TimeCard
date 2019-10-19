using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodNeighborHouse.TimeCard.Data.Entities
{
    [Table(@"Punch")]
    public class Punch {
        [Key, Column(@"ID", TypeName = @"GUID"), Required]
        public Guid Id { get; set; }

        [Column(@"VolunteerId", TypeName = @"GUID"), Required]
        public Guid VolunteerId { get; set; }
    
        [Column(@"DeptId"), Required]
        public String DeptId { get; set; }

        [Column(@"IsClockIn", TypeName=@"BIT"), Required]
        public bool IsClockIn { get; set; }

        [Column(@"PunchTime", TypeName = @"DATETIME"), Required]
        public DateTime PunchTime { get; set; }
    
        [Column(@"IsDeleted", TypeName = @"BIT"), Required]
        public bool IsDeleted { get; set; }
    
        [Column(@"CreatedBy", TypeName = @"GUID"), Required]
        public Guid CreatedBy { get; set; }
    
        [Column(@"CreatedAt", TypeName = @"DATETIME"), Required]
        public DateTime CreatedAt { get; set; }
    
        [Column(@"LastUpdatedBy", TypeName = @"GUID"), Required]
        public Guid LastUpdatedBy { get; set; }
    
        [Column(@"UpdatedAt", TypeName = @"DATETIME"), Required]
        public DateTime UpdatedAt { get; set; }
    }
}