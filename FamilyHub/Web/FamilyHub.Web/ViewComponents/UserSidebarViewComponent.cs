namespace FamilyHub.Web.ViewComponents
{
    using FamilyHub.Data.Models;
    using FamilyHub.Services.Data.User;
    using FamilyHub.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UserSidebarViewComponent : ViewComponent
    {
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public UserSidebarViewComponent(
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var userId = this.userManager.GetUserId(this.UserClaimsPrincipal);
            var model = this.usersService.GetById<UserSidebarViewModel>(userId);

            // Temporary Solution For the Tests.
            if (model == null)
            {
                model = new UserSidebarViewModel
                {
                    UserName = "Test",
                    ProfilePictureUrl =
                    "https://res.cloudinary.com/daal2scr5/image/upload/v1587625670/DefaultProfilePic_syimdd.png",
                };
            }

            return this.View("Default", model);
        }
    }
}