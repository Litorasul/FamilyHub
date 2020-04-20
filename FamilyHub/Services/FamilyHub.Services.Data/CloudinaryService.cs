namespace FamilyHub.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using FamilyHub.Common;
    using FamilyHub.Data.Common.Repositories;
    using FamilyHub.Data.Models.PictureAlbums;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly IOptions<CloudinarySettings> cloudinaryConfig;
        private readonly IDeletableEntityRepository<Picture> pictureRepository;
        private Cloudinary cloudinary;

        public CloudinaryService(
            IOptions<CloudinarySettings> cloudinaryConfig,
            IDeletableEntityRepository<Picture> pictureRepository)
        {
            this.cloudinaryConfig = cloudinaryConfig;
            this.pictureRepository = pictureRepository;
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
                var pictureUrl = uploadResult.Uri.ToString();
                string thumbEnd = $"v{uploadResult.Version}/{publicId}.jpg";
                var pictureThumb = $"https://res.cloudinary.com/daal2scr5/image/upload/c_thumb,h_200/{thumbEnd}";

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