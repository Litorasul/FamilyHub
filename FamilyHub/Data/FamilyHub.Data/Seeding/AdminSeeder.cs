namespace FamilyHub.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyHub.Common;
    using FamilyHub.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var admin = await userManager.FindByEmailAsync("admin@example.com");

            if (admin != null)
            {
                return;
            }

            var result = await userManager.CreateAsync(
                new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "admin@example.com",
                    EmailConfirmed = true,
                }, "A1d2m3i4n5");

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
            }

            var user = await userManager.FindByEmailAsync("admin@example.com");

            await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
        }
    }
}