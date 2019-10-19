using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GoodNeighborHouse.TimeCard.Data.Entities
{
    [Table(@"Recon")]
    public class Reconciliation : AbstractIdentifiable
    {
       
        [Column(@"In")]
        public Guid PunchInId { get; set; }
        
        [Column(@"Out")]
        public Guid PunchOutId { get; set; }
        
        [Column(@"ApprovedBy")]
        public int ApprovedBy { get; set; }

        [Column(@"ApprovedOn")]
        public DateTime ApprovedOn { get; set; }

        [Column(@"Difference")]
        public long Difference { get; set; }
        //difference in milliseconds.
        [ForeignKey(nameof(PunchInId))] public virtual Punch PunchIn { get; set; }
        [ForeignKey(nameof(PunchOutId))] public virtual Punch PunchOut { get; set; }
    }
}
