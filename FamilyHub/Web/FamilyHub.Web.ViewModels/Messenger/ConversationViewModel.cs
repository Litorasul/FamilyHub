namespace FamilyHub.Web.ViewModels.Messenger
{
    using System;
    using System.Collections.Generic;

    using FamilyHub.Data.Models.Messenger;
    using FamilyHub.Services.Mapping;

    public class ConversationViewModel : IMapFrom<Conversation>
    {
        public ConversationViewModel()
        {
            this.Users = new HashSet<ConversationUserViewModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<ConversationUserViewModel> Users { get; set; }

        public ICollection<MessageViewModel> Messages { get; set; }

    }
}