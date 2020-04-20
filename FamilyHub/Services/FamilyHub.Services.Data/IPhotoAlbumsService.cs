namespace FamilyHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IPhotoAlbumsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);

        Task<bool> CreateAlbum(string title, string description, IFormFile picture, string userId);

        T GetById<T>(int id);

        Task DeleteAlbum(int albumId);

        IEnumerable<T> GetAllDeleted<T>(int? count = null);

        Task UnDelete(int albumId);
    }
}
