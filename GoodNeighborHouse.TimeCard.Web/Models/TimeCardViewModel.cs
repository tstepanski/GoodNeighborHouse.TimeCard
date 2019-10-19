using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodNeighborHouse.TimeCard.Web.Models
{
    public class TimeCardViewModel
    {
        public Guid ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        //public List<Punch> Punches { get; set; }
    }
}
