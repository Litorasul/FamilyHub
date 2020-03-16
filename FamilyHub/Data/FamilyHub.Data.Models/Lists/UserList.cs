namespace FamilyHub.Data.Models.Lists
{
    public class UserList
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int ListId { get; set; }

        public virtual List List { get; set; }
    }
}