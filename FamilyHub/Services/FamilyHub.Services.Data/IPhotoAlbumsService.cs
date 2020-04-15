namespace FamilyHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IPhotoAlbumsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);

        Task AddPhotoInAlbum(int albumId, IFormFile file);

        Task<bool> CreateAlbum(string title, string description, IFormFile picture, string userId);

        T GetById<T>(int id);

        IEnumerable<T> GetAllDeleted<T>(int? count = null);

        Task UnDelete(int albumId);
    }
}
