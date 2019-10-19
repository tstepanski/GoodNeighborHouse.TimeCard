using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodNeighborHouse.TimeCard.Web.Models
{
    public class TimeCardViewModel
    {
        public Guid UserID { get; set; }

        public DateTime LastPunch { get; set; }

        //public List<Punch> Punches { get; set; }
    }
}
