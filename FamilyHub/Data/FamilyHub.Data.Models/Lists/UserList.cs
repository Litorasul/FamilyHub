namespace FamilyHub.Data.Models.Lists
{
    public class UserList
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ListId { get; set; }

        public List List { get; set; }
    }
}