namespace FamilyHub.Web.ViewModels.Users
{
    using FamilyHub.Data.Models;
    using FamilyHub.Services.Mapping;

    public class UserDropDownViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
