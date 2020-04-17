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
    using FamilyHub.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class UserServiceTests
    {
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
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));

            await repository.AddAsync(userOne);
            await repository.AddAsync(userTwo);
            await repository.SaveChangesAsync();

            var service = new UsersService(repository);
            AutoMapperConfig.RegisterMappings(typeof(TestUserViewModel).Assembly);

            List<TestUserViewModel> models = service.GetAll<TestUserViewModel>().ToList();

            Assert.Equal(2, models.Count);
            Assert.Equal("aaa", models[0].UserName);
            Assert.Equal("bbb", models[1].UserName);
        }

        public class TestUserViewModel : IMapFrom<ApplicationUser>
        {
            public string UserName { get; set; }
        }
    }
}