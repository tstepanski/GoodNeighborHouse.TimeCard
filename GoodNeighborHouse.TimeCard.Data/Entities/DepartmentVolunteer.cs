using System;

namespace GoodNeighborHouse.TimeCard.Data.Entities
{
    public class DepartmentVolunteer : AbstractIdentifiable
    {
        public Guid DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public Guid VolunteerId { get; set; }
        public virtual Volunteer Volunteer { get; set; }
    }
}
