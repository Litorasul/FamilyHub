namespace FamilyHub.Services.Data
{
    using System.Collections.Generic;

    public interface IPhotoAlbumsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);
    }
}