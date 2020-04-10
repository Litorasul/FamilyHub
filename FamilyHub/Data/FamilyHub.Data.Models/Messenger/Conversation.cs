namespace FamilyHub.Data.Models.Messenger
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FamilyHub.Data.Common.Models;

    public class Conversation : BaseDeletableModel<int>
    {

        public Conversation()
        {
            this.Users = new HashSet<UserConversation>();
            this.Messages = new HashSet<Message>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<UserConversation> Users { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}