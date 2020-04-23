namespace FamilyHub.Web.ViewModels.Users
{
    using FamilyHub.Data.Models;
    using FamilyHub.Services.Mapping;

    public class UserSidebarViewModel : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}