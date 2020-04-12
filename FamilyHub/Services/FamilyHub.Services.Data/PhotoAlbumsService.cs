using System.Threading.Tasks;

namespace FamilyHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CloudinaryDotNet;
    using FamilyHub.Common;
    using FamilyHub.Data.Common.Repositories;
    using FamilyHub.Data.Models.PictureAlbums;
    using FamilyHub.Services.Mapping;
    using Microsoft.Extensions.Options;

    public class PhotoAlbumsService : IPhotoAlbumsService
    {
        private readonly IDeletableEntityRepository<Album> albumRepository;
        private readonly IOptions<CloudinarySettings> cloudinaryConfig;
        private Cloudinary cloudinary;

        public PhotoAlbumsService(
            IDeletableEntityRepository<Album> albumRepository,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            this.albumRepository = albumRepository;
            this.cloudinaryConfig = cloudinaryConfig;

            Account account = new Account(
                cloudinaryConfig.Value.CloudName,
                cloudinaryConfig.Value.ApiKey,
                cloudinaryConfig.Value.ApiSecret);

            //this.cloudinary = new Cloudinary(account);
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

        //public async Task AddPhotoInAlbum(int albumId)
        //{

        //}
    }
}
