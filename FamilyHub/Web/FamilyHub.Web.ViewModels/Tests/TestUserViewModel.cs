namespace FamilyHub.Web.ViewModels.Tests
{
    using FamilyHub.Data.Models;
    using FamilyHub.Services.Mapping;

    public class TestUserViewModel : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }
    }
}