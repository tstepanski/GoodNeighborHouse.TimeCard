using System;

namespace GoodNeighborHouse.TimeCard.Web.Models
{
    public sealed class Punch
    {
        public Guid Id { get; set; }

        public Guid VolunteerId { get; set; }

        public Guid DepartmentId { get; set; }

        public bool IsClockIn { get; set; }

        public DateTime PunchTime { get; set; }

        public bool IsDeleted { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid LastUpdatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
