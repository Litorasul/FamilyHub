namespace FamilyHub.Web.ViewModels.Messenger
{
    using FamilyHub.Data.Models;
    using FamilyHub.Data.Models.Messenger;
    using FamilyHub.Services.Mapping;

    public class ConversationUserViewModel : IMapFrom<UserConversation>
    {
        public string UserUserName { get; set; }

        public string UserProfilePictureUrl { get; set; }
    }
}