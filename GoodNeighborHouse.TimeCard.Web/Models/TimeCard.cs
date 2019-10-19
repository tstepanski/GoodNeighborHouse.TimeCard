using System;
using System.Collections.Generic;

namespace GoodNeighborHouse.TimeCard.Web.Models
{
    public sealed class TimeCard
    {
        public Guid VolunteerId { get; set; }

        public DateTime LastPunch { get; set; }

        public List<Punch> VolunteerPunches { get; set; }
    }
}
