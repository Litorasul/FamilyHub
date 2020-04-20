namespace FamilyHub.Services.Data.Tests.Lists
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyHub.Data;
    using FamilyHub.Data.Common.Repositories;
    using FamilyHub.Data.Models.Lists;
    using FamilyHub.Data.Models.WallPosts;
    using FamilyHub.Data.Repositories;
    using FamilyHub.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class ListServiceTests
    {
        private readonly IDeletableEntityRepository<List> listRepository;
        private readonly IDeletableEntityRepository<ListItem> listItemRepository;
        private readonly IWallPostsService postsService;
        private readonly IDeletableEntityRepository<Post> postRepository;
        private readonly ApplicationDbContext dbContext;

        public ListServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            this.dbContext = new ApplicationDbContext(options);
            this.listRepository = new EfDeletableEntityRepository<List>(this.dbContext);
            this.listItemRepository = new EfDeletableEntityRepository<ListItem>(this.dbContext);
            this.postRepository = new EfDeletableEntityRepository<Post>(this.dbContext);
            this.postsService = new Mock<IWallPostsService>().Object;
            AutoMapperConfig.RegisterMappings(typeof(TestListViewModel).Assembly);
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
            this.listRepository.Dispose();
            this.listItemRepository.Dispose();
            this.postRepository.Dispose();
        }

        [Fact]
        public async Task GetAllShouldReturnAllLists()
        {
            await this.PopulateLists();

            var service = new ListsService(
                this.listRepository,
                this.listItemRepository,
                this.postsService,
                this.postRepository);

            List<TestListViewModel> models = service.GetAll<TestListViewModel>().ToList();

            Assert.Equal(3, models.Count);
            Assert.Equal(1, models[0].Id);
            Assert.Equal(2, models[1].Id);
            Assert.Equal(3, models[2].Id);
        }

        [Fact]
        public async Task GetAllByTypeShouldReturnCorrectLists()
        {
            await this.PopulateLists();

            var service = new ListsService(
                this.listRepository,
                this.listItemRepository,
                this.postsService,
                this.postRepository);

            List<TestListViewModel> models = service
                .GetAllByType<TestListViewModel>(ListType.ToDoList).ToList();


            Assert.Single(models);
            Assert.Equal(ListType.ToDoList, models[0].Type);
        }

        [Fact]
        public async Task GetByNameShouldReturnTheCorrectList()
        {
            await this.PopulateLists();

            var service = new ListsService(
                this.listRepository,
                this.listItemRepository,
                this.postsService,
                this.postRepository);

            var list = service.GetByName<TestListViewModel>("bbb");

            Assert.Equal(2, list.Id);
        }

        [Fact]
        public async Task GetByIdShouldReturnTheCorrectList()
        {
            await this.PopulateLists();

            var service = new ListsService(
                this.listRepository,
                this.listItemRepository,
                this.postsService,
                this.postRepository);

            var list = service.GetById<TestListViewModel>(3);

            Assert.Equal("ccc", list.Title);
        }

        [Fact]
        public async Task CreateAsyncShouldCreateNewList()
        {
            var lists = new List<List>();
            var repository = new Mock<IDeletableEntityRepository<List>>();
            repository
                .Setup(r => r.AddAsync(It.IsAny<List>()))
                .Callback((List p) => lists.Add(p));

            var service = new ListsService(
                repository.Object,
                this.listItemRepository,
                this.postsService,
                this.postRepository);

            await service
                .CreateAsync("aaa", "bbb", ListType.ToDoList, "ccc");

            Assert.Equal("aaa", lists[0].Title);
            Assert.Equal("bbb", lists[0].Description);
            Assert.Equal("ccc", lists[0].CreatorId);
            Assert.Equal(ListType.ToDoList, lists[0].Type);
        }

        [Fact]
        public async Task CreateAsyncShouldCreateNewWallPost()
        {
            var post = new Post();
            var postService = new Mock<IWallPostsService>();
            postService
                .Setup(r
                    => r.CreateAsync
                        (It.IsAny<string>(), It.IsAny<PostType>(), It.IsAny<int?>(), It.IsAny<string>()))
                .Callback<string, PostType, int?, string>((c, t, a, o) =>
                {
                    post.UserId = c;
                    post.PostType = t;
                    post.AssignedEntity = a;
                    post.Content = o;
                });

            var service = new ListsService(
                this.listRepository,
                this.listItemRepository,
                postService.Object,
                this.postRepository);

            await service
                .CreateAsync("aaa", "bbb", ListType.ToDoList, "ccc");

            Assert.Equal("ccc", post.UserId);
            Assert.Equal(PostType.NewList, post.PostType);
            Assert.Null(post.Content);
        }

        [Fact]
        public async Task AddItemToListShouldCreateNewListItem()
        {
            var listItems = new List<ListItem>();
            var repository = new Mock<IDeletableEntityRepository<ListItem>>();
            repository
                .Setup(r => r.AddAsync(It.IsAny<ListItem>()))
                .Callback((ListItem p) => listItems.Add(p));

            var service = new ListsService(
                this.listRepository,
                repository.Object,
                this.postsService,
                this.postRepository);

            await service
                .AddItemToList(1, "aaa");

            Assert.Equal(1, listItems[0].ListId);
            Assert.Equal("aaa", listItems[0].Text);
        }

        [Fact]
        public async Task ListItemUpdateShouldUpdateCorrectListItem()
        {
            await this.PopulateListItems();

            var service = new ListsService(
                this.listRepository,
                this.listItemRepository,
                this.postsService,
                this.postRepository);

            await service.ListItemUpdate(1, "ccc");
            var item = this.listItemRepository.All().FirstOrDefault(i => i.Id == 1);

            Assert.Equal("ccc", item.Text);
        }

        [Fact]
        public async Task ListItemUpdateDoneShouldUpdateCorrectListItem()
        {
            await this.PopulateListItems();

            var service = new ListsService(
                this.listRepository,
                this.listItemRepository,
                this.postsService,
                this.postRepository);

            await service.ListItemUpdateDone(2, "ddd", DateTime.Now);
            var item = this.listItemRepository.All().FirstOrDefault(i => i.Id == 2);

            Assert.Equal("ddd", item.DoneByUserId);
        }

        [Fact]
        public async Task GetAllDeletedShouldReturnOnlyDeletedLists()
        {
            await this.PopulateLists();
            var list = this.listRepository.All().FirstOrDefault(l => l.Id == 1);
            this.listRepository.Delete(list);
            await this.listRepository.SaveChangesAsync();

            var service = new ListsService(
                this.listRepository,
                this.listItemRepository,
                this.postsService,
                this.postRepository);

            var resultList = service.GetAllDeleted<TestListViewModel>().ToList();

            Assert.Single(resultList);
            Assert.Equal("aaa", resultList[0].Title);
        }

        [Fact]
        public async Task UnDeleteShouldUnDeleteCorrectList()
        {
            await this.PopulateLists();
            var list = this.listRepository.All().FirstOrDefault(l => l.Id == 2);
            this.listRepository.Delete(list);
            await this.listRepository.SaveChangesAsync();

            var service = new ListsService(
                this.listRepository,
                this.listItemRepository,
                this.postsService,
                this.postRepository);

            await service.UnDelete(2);

            var listResult = this.listRepository.All().FirstOrDefault(l => l.Id == 2);

            Assert.Equal("bbb", listResult.Title);
            Assert.False(listResult.IsDeleted);
        }

        [Fact]
        public async Task DeleteListShouldDeleteCorrectListAndCorrespondingPost()
        {
            await this.PopulateLists();
            await this.PopulatePosts();

            var service = new ListsService(
                this.listRepository,
                this.listItemRepository,
                this.postsService,
                this.postRepository);

            await service.DeleteList(3);

            var list = this.listRepository.All().FirstOrDefault(l => l.Title == "ccc");
            var post = this.postRepository.All().FirstOrDefault(p => p.Id == 3);

            Assert.Null(list);
            Assert.Null(post);
        }

        private async Task PopulatePosts()
        {
            var postOne = new Post
            {
                Id = 1,
                PostType = PostType.NewList,
                AssignedEntity = 1,
            };

            var postTwo = new Post
            {
                Id = 2,
                PostType = PostType.NewList,
                AssignedEntity = 2,
            };

            var postThree = new Post
            {
                Id = 3,
                PostType = PostType.NewList,
                AssignedEntity = 3,
            };

            this.dbContext.Posts.Add(postOne);
            this.dbContext.Posts.Add(postTwo);
            this.dbContext.Posts.Add(postThree);
            await this.dbContext.SaveChangesAsync();
        }

        private async Task PopulateListItems()
        {
            var itemOne = new ListItem
            {
                Id = 1,
                Text = "aaa",
            };

            var itemTwo = new ListItem
            {
                Id = 2,
                Text = "bbb",
            };

            this.dbContext.ListItems.Add(itemOne);
            this.dbContext.ListItems.Add(itemTwo);
            await this.dbContext.SaveChangesAsync();
        }

        private async Task PopulateLists()
        {
            var toDoList = new List
            {
                Id = 1,
                Type = ListType.ToDoList,
                Title = "aaa",
            };
            var shoppingList = new List
            {
                Id = 2,
                Type = ListType.ShoppingList,
                Title = "bbb",
            };
            var choresList = new List
            {
                Id = 3,
                Type = ListType.ChoresList,
                Title = "ccc",
            };
            this.dbContext.Lists.Add(toDoList);
            this.dbContext.Lists.Add(shoppingList);
            this.dbContext.Lists.Add(choresList);
            await this.dbContext.SaveChangesAsync();
        }

        public class TestListViewModel : IMapFrom<List>
        {
            public int Id { get; set; }

            public ListType Type { get; set; }

            public string Title { get; set; }
        }
    }
}