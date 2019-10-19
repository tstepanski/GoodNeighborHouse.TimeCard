using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GoodNeighborHouse.TimeCard.Data.Entities
{
    [Table(@"Reconciliations")]
    public class Reconciliation : AbstractIdentifiable
    {
       
        [Column(@"In"), Required]
        public Guid PunchInId { get; set; }
        
        [Column(@"Out"), Required]
        public Guid PunchOutId { get; set; }
        
        [Column(@"ApprovedBy"), Required]
        public int ApprovedBy { get; set; }

        [Column(@"ApprovedOn", TypeName = @"DATETIME"), Required]
        public DateTime ApprovedOn { get; set; }

        [Column(@"Difference", TypeName = @"BIGINT"),Required]
        public long Difference { get; set; }
        //difference in milliseconds.
        [ForeignKey(nameof(PunchInId))] public virtual Punch PunchIn { get; set; }
        [ForeignKey(nameof(PunchOutId))] public virtual Punch PunchOut { get; set; }
    }
}
