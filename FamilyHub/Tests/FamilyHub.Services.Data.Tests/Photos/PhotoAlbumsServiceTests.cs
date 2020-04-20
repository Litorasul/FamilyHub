namespace FamilyHub.Services.Data.Tests.Photos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyHub.Common;
    using FamilyHub.Data;
    using FamilyHub.Data.Common.Repositories;
    using FamilyHub.Data.Models.PictureAlbums;
    using FamilyHub.Data.Models.WallPosts;
    using FamilyHub.Data.Repositories;
    using FamilyHub.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Moq;
    using Xunit;

    public class PhotoAlbumsServiceTests
    {
        private readonly IDeletableEntityRepository<Album> albumRepository;
        private readonly IDeletableEntityRepository<Picture> pictureRepository;
        private readonly IWallPostsService postsService;
        private readonly IDeletableEntityRepository<Post> postRepository;
        private readonly ApplicationDbContext dbContext;
        private readonly ICloudinaryService cloudinaryService;

        public PhotoAlbumsServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            this.dbContext = new ApplicationDbContext(options);

            this.albumRepository = new EfDeletableEntityRepository<Album>(this.dbContext);
            this.postRepository = new EfDeletableEntityRepository<Post>(this.dbContext);
            this.pictureRepository = new EfDeletableEntityRepository<Picture>(this.dbContext);
            this.postsService = new Mock<IWallPostsService>().Object;
            this.cloudinaryService = new Mock<ICloudinaryService>().Object;

            AutoMapperConfig.RegisterMappings(typeof(TestAlbumViewModel).Assembly);
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
            this.albumRepository.Dispose();
            this.pictureRepository.Dispose();
            this.postRepository.Dispose();
        }

        [Fact]
        public async Task GetAllShouldReturnAllAlbums()
        {
            await this.PopulateAlbums();

            var service = new PhotoAlbumsService(
                this.albumRepository,
                this.pictureRepository,
                this.postsService,
                this.postRepository,
                this.cloudinaryService);

            List<TestAlbumViewModel> models = service.GetAll<TestAlbumViewModel>().ToList();

            Assert.Equal(3, models.Count);
        }

        [Fact]
        public async Task GetByNameShouldReturnTheCorrectAlbum()
        {
            await this.PopulateAlbums();

            var service = new PhotoAlbumsService(
                this.albumRepository,
                this.pictureRepository,
                this.postsService,
                this.postRepository,
                this.cloudinaryService);

            var album = service.GetByName<TestAlbumViewModel>("bbb");

            Assert.Equal(2, album.Id);
        }

        [Fact]
        public async Task GetByIdShouldReturnTheCorrectAlbum()
        {
            await this.PopulateAlbums();

            var service = new PhotoAlbumsService(
                this.albumRepository,
                this.pictureRepository,
                this.postsService,
                this.postRepository,
                this.cloudinaryService);

            var album = service.GetById<TestAlbumViewModel>(3);

            Assert.Equal("ccc", album.Title);
        }

        [Fact]
        public async Task CreateAlbumShouldCreateAlbum()
        {
            var albums = new List<Album>();

            var repository = new Mock<IDeletableEntityRepository<Album>>();
            repository
                .Setup(r => r.AddAsync(It.IsAny<Album>()))
                .Callback((Album a) => albums.Add(a));

            var cloudService = new Mock<ICloudinaryService>();

            cloudService.Setup(c => c.AddPhotoInAlbum(It.IsAny<int>(), It.IsAny<IFormFile>()));

            var service = new PhotoAlbumsService(
                repository.Object,
                this.pictureRepository,
                this.postsService,
                this.postRepository,
                cloudService.Object);

            await service.CreateAlbum("aaa", "bbb", null, "ccc");

            Assert.Single(albums);
            Assert.Equal("aaa", albums[0].Title);
            Assert.Equal("bbb", albums[0].Description);
            Assert.Equal("ccc", albums[0].UserId);
            Assert.IsAssignableFrom<ICollection<Picture>>(albums[0].Pictures);
        }

        [Fact]
        public async Task CreateAlbumShouldCreateNewWallPost()
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

            var service = new PhotoAlbumsService(
                this.albumRepository,
                this.pictureRepository,
                postService.Object,
                this.postRepository,
                this.cloudinaryService);

            await service.CreateAlbum("aaa", "bbb", null, "ccc");

            Assert.Equal("ccc", post.UserId);
            Assert.Equal(PostType.NewPicture, post.PostType);
            Assert.Null(post.Content);
        }

        [Fact]
        public async Task GetAllDeletedShouldReturnOnlyDeletedAlbums()
        {
            await this.PopulateAlbums();
            var album = this.albumRepository.All().FirstOrDefault(a => a.Id == 1);
            this.albumRepository.Delete(album);
            await this.albumRepository.SaveChangesAsync();

            var service = new PhotoAlbumsService(
                this.albumRepository,
                this.pictureRepository,
                this.postsService,
                this.postRepository,
                this.cloudinaryService);

            var resultList = service
                .GetAllDeleted<TestAlbumViewModel>().ToList();

            Assert.Single(resultList);
            Assert.Equal("aaa", resultList[0].Title);
        }

        [Fact]
        public async Task UnDeleteShouldUnDeleteCorrectAlbum()
        {
            await this.PopulateAlbums();
            var album = this.albumRepository.All().FirstOrDefault(a => a.Id == 2);
            this.albumRepository.Delete(album);
            await this.albumRepository.SaveChangesAsync();

            var service = new PhotoAlbumsService(
                this.albumRepository,
                this.pictureRepository,
                this.postsService,
                this.postRepository,
                this.cloudinaryService);

            await service.UnDelete(2);

            var result = this.albumRepository.All().FirstOrDefault(a => a.Id == 2);

            Assert.False(result.IsDeleted);
            Assert.Equal("bbb", result.Title);
        }

        [Fact]
        public async Task DeleteAlbumShouldDeleteCorrectListAndCorrespondingPost()
        {
            await this.PopulateAlbums();
            await this.PopulatePosts();

            var service = new PhotoAlbumsService(
                this.albumRepository,
                this.pictureRepository,
                this.postsService,
                this.postRepository,
                this.cloudinaryService);

            await service.DeleteAlbum(3);

            var album = this.albumRepository.All().FirstOrDefault(a => a.Title == "ccc");
            var post = this.postRepository.All().FirstOrDefault(p => p.Id == 3);

            Assert.Null(album);
            Assert.Null(post);
        }

        private async Task PopulatePosts()
        {
            var postOne = new Post
            {
                Id = 1,
                PostType = PostType.NewPicture,
                AssignedEntity = 1,
            };

            var postTwo = new Post
            {
                Id = 2,
                PostType = PostType.NewPicture,
                AssignedEntity = 2,
            };

            var postThree = new Post
            {
                Id = 3,
                PostType = PostType.NewPicture,
                AssignedEntity = 3,
            };

            this.dbContext.Posts.Add(postOne);
            this.dbContext.Posts.Add(postTwo);
            this.dbContext.Posts.Add(postThree);
            await this.dbContext.SaveChangesAsync();
        }

        private async Task PopulateAlbums()
        {
            var albumOne = new Album
            {
                Id = 1,
                Title = "aaa",
                UserId = "user",
            };

            var albumTwo = new Album
            {
                Id = 2,
                Title = "bbb",
                UserId = "user",
            };

            var albumThree = new Album
            {
                Id = 3,
                Title = "ccc",
                UserId = "user",
            };

            this.dbContext.PictureAlbums.Add(albumOne);
            this.dbContext.PictureAlbums.Add(albumTwo);
            this.dbContext.PictureAlbums.Add(albumThree);
            await this.dbContext.SaveChangesAsync();
        }

        public class TestAlbumViewModel : IMapFrom<Album>
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }

            public string UserId { get; set; }
        }
    }
}
