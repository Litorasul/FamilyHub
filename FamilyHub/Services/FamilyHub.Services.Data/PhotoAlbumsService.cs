namespace FamilyHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using FamilyHub.Data.Common.Repositories;
    using FamilyHub.Data.Models.PictureAlbums;
    using FamilyHub.Services.Mapping;

    public class PhotoAlbumsService : IPhotoAlbumsService
    {
        private readonly IDeletableEntityRepository<Album> albumRepository;

        public PhotoAlbumsService(IDeletableEntityRepository<Album> albumRepository)
        {
            this.albumRepository = albumRepository;
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
    }
}