namespace FamilyHub.Web.Areas.Identity
{
    using System.Threading.Tasks;

    using FamilyHub.Data.Models;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Overriden PasswordSignInAsync method so you can Log In with email instead of username.
    /// </summary>
    public class CustomSignInManager : SignInManager<ApplicationUser>
    {
        public CustomSignInManager(
            UserManager<ApplicationUser> userManager, 
            IHttpContextAccessor contextAccessor, 
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<ApplicationUser>> logger,
            IAuthenticationSchemeProvider schemes, IUserConfirmation<ApplicationUser> confirmation)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }

        public override async Task<SignInResult> PasswordSignInAsync(string email, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var user = await this.UserManager.FindByEmailAsync(email);

            if (user == null)
            {
                return SignInResult.Failed;
            }

            return await this.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }
    }
}