using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Punch")]
public class Punch {
    public int PunchId { get; set; }

    [Required]
    public int VolunteerId { get; set; }
    
    [Required]
    public DateTime PunchTime { get; set; }
    
    [Required]
    public int IsDeleted { get; set; }
    
    [Required]
    public int CreatedBy { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    [Required]
    public int LastUpdatedBy { get; set; }
    
    [Required]
    public DateTime UpdatedAt { get; set; }
}