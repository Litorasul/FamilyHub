namespace FamilyHub.Services.Data.Tests.WallPosts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyHub.Data;
    using FamilyHub.Data.Common.Repositories;
    using FamilyHub.Data.Models.WallPosts;
    using FamilyHub.Data.Repositories;
    using FamilyHub.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class WallPostServiceTests
    {
        private readonly IDeletableEntityRepository<Post> postRepository;
        private readonly ApplicationDbContext dbContext;

        public WallPostServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            this.dbContext = new ApplicationDbContext(options);
            this.postRepository = new EfDeletableEntityRepository<Post>(this.dbContext);
            AutoMapperConfig.RegisterMappings(typeof(TestPostViewModel).Assembly);
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
            this.postRepository.Dispose();
        }

        [Fact]
        public async Task CreateAsyncShouldCreateStatusUpdateTypeWallPost()
        {
            var posts = new List<Post>();
            var repository = new Mock<IDeletableEntityRepository<Post>>();
            repository
                .Setup(r => r.AddAsync(It.IsAny<Post>()))
                .Callback((Post p) => posts.Add(p));

            var service = new WallPostsService(repository.Object);
            await service
                .CreateAsync("aaa", PostType.StatusUpdate, null, "Test");

            Assert.Equal("aaa", posts[0].UserId);
            Assert.Equal(PostType.StatusUpdate, posts[0].PostType);
            Assert.Null(posts[0].AssignedEntity);
            Assert.Equal("Test", posts[0].Content);
        }

        [Fact]
        public async Task CreateAsyncShouldCreateNewListTypeWallPost()
        {
            var posts = new List<Post>();
            var repository = new Mock<IDeletableEntityRepository<Post>>();
            repository
                .Setup(r => r.AddAsync(It.IsAny<Post>()))
                .Callback((Post p) => posts.Add(p));

            var service = new WallPostsService(repository.Object);
            await service
                .CreateAsync("aaa", PostType.NewList, 1, null);

            Assert.Equal("aaa", posts[0].UserId);
            Assert.Equal(PostType.NewList, posts[0].PostType);
            Assert.Equal(1, posts[0].AssignedEntity);
            Assert.Null(posts[0].Content);
        }

        [Fact]
        public async Task GetAllShouldReturnCorrectPosts()
        {
            var postOne = new Post
            {
                Id = 1,
                UserId = "aaa",
                PostType = PostType.StatusUpdate,
                Content = "One",
                CreatedOn = DateTime.Now,
            };

            var postTwo = new Post
            {
                Id = 2,
                UserId = "bbb",
                PostType = PostType.StatusUpdate,
                Content = "One",
                CreatedOn = DateTime.UtcNow,
            };

            this.dbContext.Posts.Add(postOne);
            this.dbContext.Posts.Add(postTwo);
            await this.dbContext.SaveChangesAsync();

            var service = new WallPostsService(this.postRepository);
           

            List<TestPostViewModel> models = service.GetAll<TestPostViewModel>().ToList();

            Assert.Equal(2, models.Count);
            Assert.Equal(1, models[0].Id);
            Assert.Equal(2, models[1].Id);
        }

        public class TestPostViewModel : IMapFrom<Post>
        {
            public int Id { get; set; }
        }
    }
}
