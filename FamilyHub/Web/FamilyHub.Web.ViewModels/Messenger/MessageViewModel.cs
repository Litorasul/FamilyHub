namespace FamilyHub.Web.ViewModels.Messenger
{
    using System;

    using FamilyHub.Data.Models.Messenger;
    using FamilyHub.Services.Mapping;

    public class MessageViewModel : IMapFrom<Message>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}