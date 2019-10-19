using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodNeighborHouse.TimeCard.Web.Models
{
    public class Reconciliation
    {

        public Guid PunchInId { get; set; }

        public Guid PunchOutId { get; set; }

        public int ApprovedBy { get; set; }

        public DateTime ApprovedOn { get; set; }

    }
}
