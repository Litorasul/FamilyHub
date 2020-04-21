namespace FamilyHub.Services.Data.Tests.Planner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyHub.Data;
    using FamilyHub.Data.Common.Repositories;
    using FamilyHub.Data.Models.Planner;
    using FamilyHub.Data.Models.WallPosts;
    using FamilyHub.Data.Repositories;
    using FamilyHub.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class EventServiceTests : IDisposable
    {
        private readonly IDeletableEntityRepository<Event> eventsRepository;
        private readonly IWallPostsService postsService;
        private readonly IDeletableEntityRepository<Post> postRepository;
        private readonly ApplicationDbContext dbContext;

        public EventServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            this.dbContext = new ApplicationDbContext(options);
            this.eventsRepository = new EfDeletableEntityRepository<Event>(this.dbContext);
            this.postRepository = new EfDeletableEntityRepository<Post>(this.dbContext);
            AutoMapperConfig.RegisterMappings(typeof(TestEventViewModel).Assembly);
            this.postsService = new Mock<IWallPostsService>().Object;
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
            this.eventsRepository.Dispose();
            this.postRepository.Dispose();
        }

        [Fact]
        public async Task GetAllShouldReturnAllEvents()
        {
            await this.PopulateEvents();

            var service = new EventsService(
                this.eventsRepository,
                this.postsService,
                this.postRepository);

            List<TestEventViewModel> models = service.GetAll<TestEventViewModel>().ToList();

            Assert.Equal(3, models.Count);
        }

        [Fact]
        public async Task GetByIdShouldReturnCorrectEvent()
        {
            await this.PopulateEvents();

            var service = new EventsService(
                this.eventsRepository,
                this.postsService,
                this.postRepository);

            var result = service.GetById<TestEventViewModel>(2);

            Assert.Equal("ccc", result.Title);
        }

        [Fact]
        public async Task GetByNameShouldReturnCorrectEvent()
        {
            await this.PopulateEvents();

            var service = new EventsService(
                this.eventsRepository,
                this.postsService,
                this.postRepository);

            var result = service.GetByName<TestEventViewModel>("eee");

            Assert.Equal(3, result.Id);
        }

        [Fact]
        public async Task UpdateEventShouldUpdateTheCorrectEvent()
        {
            await this.PopulateEvents();

            var service = new EventsService(
                this.eventsRepository,
                this.postsService,
                this.postRepository);

            await service.UpdateEvent(1, "rrr", "ttt", DateTime.Now, DateTime.MaxValue, false, false, "hhh");
            var result = this.eventsRepository.All().FirstOrDefault(e => e.Id == 1);

            Assert.Equal("rrr", result.Title);
        }

        [Fact]
        public async Task DeleteEventShouldDeleteTheCorrectEventAndCorrespondingPost()
        {
            await this.PopulateEvents();
            await this.PopulatePosts();

            var service = new EventsService(
                this.eventsRepository,
                this.postsService,
                this.postRepository);

            await service.DeleteEvent(2);

            var eventTo = this.eventsRepository.All().FirstOrDefault(e => e.Title == "ccc");
            var post = this.postRepository.All().FirstOrDefault(p => p.Id == 2);

            Assert.Null(eventTo);
            Assert.Null(post);
        }

        [Fact]
        public async Task GetAllDeletedShouldReturnCorrectDeletedEntities()
        {
            await this.PopulateEvents();
            var eventToDelete = this.eventsRepository.All().FirstOrDefault(e => e.Id == 2);
            this.eventsRepository.Delete(eventToDelete);
            await this.eventsRepository.SaveChangesAsync();

            var service = new EventsService(
                this.eventsRepository,
                this.postsService,
                this.postRepository);

            var resultList = service.GetAllDeleted<TestEventViewModel>().ToList();

            Assert.Single(resultList);
            Assert.Equal("ccc", resultList[0].Title);
        }

        [Fact]
        public async Task UnDeleteShouldUnDeleteCorrectEvent()
        {
            await this.PopulateEvents();
            var eventToDelete = this.eventsRepository.All().FirstOrDefault(e => e.Id == 1);
            this.eventsRepository.Delete(eventToDelete);
            await this.eventsRepository.SaveChangesAsync();

            var service = new EventsService(
                this.eventsRepository,
                this.postsService,
                this.postRepository);

            await service.UnDelete(1);

            var result = this.eventsRepository.All().FirstOrDefault(e => e.Id == 1);

            Assert.False(result.IsDeleted);
            Assert.Equal("aaa", result.Title);
        }

        [Fact]
        public async Task CreateAsyncShouldCreateNewEvent()
        {
            var result = new List<Event>();
            var repository = new Mock<IDeletableEntityRepository<Event>>();
            repository
                .Setup(r => r.AddAsync(It.IsAny<Event>()))
                .Callback((Event e) => result.Add(e));

            var service = new EventsService(
                repository.Object,
                this.postsService,
                this.postRepository);

            await service.CreateAsync(
                "ddd", "ffff", DateTime.Now, DateTime.Today, false, false, "sss", "ppp", new List<string>());

            Assert.Equal("ddd", result[0].Title);
            Assert.Equal("ffff", result[0].Description);
        }

        [Fact]
        public async Task CreateAsyncShouldCreateNewWallPost()
        {
            var post = new Post();
            var postService = new Mock<IWallPostsService>();
            postService
                .Setup(r
                    => r.CreateAsync(It.IsAny<string>(), It.IsAny<PostType>(), It.IsAny<int?>(), It.IsAny<string>()))
                .Callback<string, PostType, int?, string>((c, t, a, o) =>
                {
                    post.UserId = c;
                    post.PostType = t;
                    post.AssignedEntity = a;
                    post.Content = o;
                });

            var service = new EventsService(
                this.eventsRepository,
                postService.Object,
                this.postRepository);
            await service.CreateAsync(
                "ttt", "yyyy", DateTime.Now, DateTime.Today, false, false, "ooo", "www", new List<string>());

            Assert.Equal("ooo", post.UserId);
            Assert.Equal(PostType.NewEvent, post.PostType);
            Assert.Null(post.Content);
        }

        private async Task PopulatePosts()
        {
            var postOne = new Post
            {
                Id = 1,
                PostType = PostType.NewEvent,
                AssignedEntity = 1,
            };

            var postTwo = new Post
            {
                Id = 2,
                PostType = PostType.NewEvent,
                AssignedEntity = 2,
            };

            var postThree = new Post
            {
                Id = 3,
                PostType = PostType.NewEvent,
                AssignedEntity = 3,
            };

            this.dbContext.Posts.Add(postOne);
            this.dbContext.Posts.Add(postTwo);
            this.dbContext.Posts.Add(postThree);
            await this.dbContext.SaveChangesAsync();
        }

        private async Task PopulateEvents()
        {
            var eventOne = new Event
            {
                Id = 1,
                Title = "aaa",
                Start = DateTime.Now,
                CreatorId = "bbb",
            };

            var eventTwo = new Event
            {
                Id = 2,
                Title = "ccc",
                Start = DateTime.Now,
                CreatorId = "ddd",
            };

            var eventThree = new Event
            {
                Id = 3,
                Title = "eee",
                Start = DateTime.Now,
                CreatorId = "fff",
            };

            this.dbContext.Events.Add(eventOne);
            this.dbContext.Events.Add(eventTwo);
            this.dbContext.Events.Add(eventThree);
            await this.dbContext.SaveChangesAsync();
        }

        public class TestEventViewModel : IMapFrom<Event>
        {
            public int Id { get; set; }

            public string Title { get; set; }
        }
    }
}
