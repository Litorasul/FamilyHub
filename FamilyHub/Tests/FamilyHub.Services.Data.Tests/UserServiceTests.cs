namespace FamilyHub.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyHub.Data;
    using FamilyHub.Data.Models;
    using FamilyHub.Data.Models.WallPosts;
    using FamilyHub.Data.Repositories;
    using FamilyHub.Services.Data.User;
    using FamilyHub.Services.Mapping;
    using FamilyHub.Web.ViewModels.Tests;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class UserServiceTests
    {
        public UserServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(TestUserViewModel).Assembly);
        }

        [Fact]
        public async Task GetAllShouldReturnCorrectUsers()
        {
            var userOne = new ApplicationUser
            {
                Id = "aaa",
                UserName = "aaa",
            };

            var userTwo = new ApplicationUser
            {
                Id = "bbb",
                UserName = "bbb",
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            dbContext.Users.Add(userOne);
            dbContext.Users.Add(userTwo);
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            var service = new UsersService(repository);

            List<TestUserViewModel> models = service.GetAll<TestUserViewModel>().ToList();

            Assert.Equal(2, models.Count);
            Assert.Equal("aaa", models[0].UserName);
            Assert.Equal("bbb", models[1].UserName);
        }
    }
}
