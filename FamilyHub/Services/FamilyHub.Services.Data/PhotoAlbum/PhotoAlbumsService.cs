namespace FamilyHub.Services.Data.PhotoAlbum
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyHub.Data.Common.Repositories;
    using FamilyHub.Data.Models.PictureAlbums;
    using FamilyHub.Data.Models.WallPosts;
    using FamilyHub.Services.Data.WallPosts;
    using FamilyHub.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class PhotoAlbumsService : IPhotoAlbumsService
    {
        private readonly IDeletableEntityRepository<Album> albumRepository;
        private readonly IDeletableEntityRepository<Picture> pictureRepository;
        private readonly IWallPostsService postsService;
        private readonly IDeletableEntityRepository<Post> postRepository;
        private readonly ICloudinaryService cloudinaryService;

        public PhotoAlbumsService(
            IDeletableEntityRepository<Album> albumRepository,
            IDeletableEntityRepository<Picture> pictureRepository,
            IWallPostsService postsService,
            IDeletableEntityRepository<Post> postRepository,
            ICloudinaryService cloudinaryService)
        {
            this.albumRepository = albumRepository;
            this.pictureRepository = pictureRepository;
            this.postsService = postsService;
            this.postRepository = postRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Album> query = this.albumRepository.All().OrderByDescending(a => a.CreatedOn);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetByName<T>(string name)
        {
            var album = this.albumRepository.All().Where(x => x.Title.Replace(" ", "-") == name)
                .To<T>().FirstOrDefault();

            return album;
        }

        public async Task<bool> CreateAlbum(string title, string description, IFormFile picture, string userId)
        {
            var album = new Album
            {
                Title = title,
                Description = description,
                UserId = userId,
                Pictures = new HashSet<Picture>(),
            };

            await this.albumRepository.AddAsync(album);
            await this.albumRepository.SaveChangesAsync();

            await this.cloudinaryService.AddPhotoInAlbum(album.Id, picture);

            await this.postsService.CreateAsync(userId, PostType.NewPicture, album.Id, null);

            return true;
        }

        public T GetById<T>(int id)
        {
            var album = this.albumRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return album;
        }

        public IEnumerable<T> GetAllDeleted<T>(int? count = null)
        {
            IQueryable<Album> query = this.albumRepository.AllWithDeleted()
                .Where(l => l.IsDeleted == true).OrderBy(l => l.Title);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task UnDelete(int albumId)
        {
            var album = this.albumRepository.AllWithDeleted().FirstOrDefault(e => e.Id == albumId);
            if (album != null && album.IsDeleted == true)
            {
                this.albumRepository.Undelete(album);
                await this.pictureRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteAlbum(int albumId)
        {
            var album = this.albumRepository.All().FirstOrDefault(a => a.Id == albumId);
            var post = this.postRepository.All()
                .FirstOrDefault(p => p.PostType == PostType.NewPicture && p.AssignedEntity == albumId);

            if (album != null)
            {
                this.albumRepository.Delete(album);
                this.postRepository.Delete(post);
                await this.albumRepository.SaveChangesAsync();
                await this.postRepository.SaveChangesAsync();
            }
        }
    }
}
