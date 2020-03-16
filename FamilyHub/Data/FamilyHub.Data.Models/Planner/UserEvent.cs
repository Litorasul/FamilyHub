namespace FamilyHub.Data.Models.Planner
{
    public class UserEvent
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}