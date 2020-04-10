namespace FamilyHub.Web.ViewModels.Messenger
{
    using FamilyHub.Data.Models;
    using FamilyHub.Services.Mapping;

    public class MessageUserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set;  }

        public string UserName { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}