namespace FamilyHub.Web.ViewModels.Messenger
{
    using System;

    using FamilyHub.Data.Models.Messenger;
    using FamilyHub.Services.Mapping;

    public class MessageInHubViewModel : IMapFrom<Message>
    {
        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public string UserProfilePictureUrl { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
