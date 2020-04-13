namespace FamilyHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using FamilyHub.Common;
    using FamilyHub.Data.Common.Repositories;
    using FamilyHub.Data.Models.PictureAlbums;
    using FamilyHub.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;

    public class PhotoAlbumsService : IPhotoAlbumsService
    {
        private readonly IDeletableEntityRepository<Album> albumRepository;
        private readonly IDeletableEntityRepository<Picture> pictureRepository;
        private readonly IOptions<CloudinarySettings> cloudinaryConfig;
        private Cloudinary cloudinary;

        public PhotoAlbumsService(
            IDeletableEntityRepository<Album> albumRepository,
            IDeletableEntityRepository<Picture> pictureRepository,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            this.albumRepository = albumRepository;
            this.pictureRepository = pictureRepository;
            this.cloudinaryConfig = cloudinaryConfig;
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

        public async Task AddPhotoInAlbum(int albumId, IFormFile file)
        {
            Account account = new Account(
                this.cloudinaryConfig.Value.CloudName,
                this.cloudinaryConfig.Value.ApiKey,
                this.cloudinaryConfig.Value.ApiSecret);

            this.cloudinary = new Cloudinary(account);

            var uploadResult = new ImageUploadResult();
            var uploadResultThumb = new ImageUploadResult();
            string pictureUrl;
            string pictureThumb;
            string publicId = Guid.NewGuid().ToString();

            if (file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.Name, stream),
                    PublicId = publicId,
                    Format = "jpg",
                };
                uploadResult = this.cloudinary.Upload(uploadParams);
                pictureUrl = uploadResult.Uri.ToString();
                string thumbEnd = $"v{uploadResult.Version}/{publicId}.jpg";
                pictureThumb = $"https://res.cloudinary.com/daal2scr5/image/upload/c_thumb,h_200/{thumbEnd}";

                var picture = new Picture()
                {
                    AlbumId = albumId,
                    Url = pictureUrl,
                    ThumbUrl = pictureThumb,
                };

                await this.pictureRepository.AddAsync(picture);
                await this.pictureRepository.SaveChangesAsync();
            }
        }
    }
}
