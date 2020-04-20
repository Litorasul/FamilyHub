namespace FamilyHub.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task AddPhotoInAlbum(int albumId, IFormFile file);
    }
}