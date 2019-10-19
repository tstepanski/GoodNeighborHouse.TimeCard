using System;

namespace GoodNeighborHouse.TimeCard.Web.Models
{
    public class Punch
    {
        public Guid Id { get; set; }
        public Guid VolunteerId { get; set; }

        public bool IsClockIn { get; set; }

        public DateTime PunchTime { get; set; }
        public bool IsDeleted { get; set; }

        public String CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public String LastUpdatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
