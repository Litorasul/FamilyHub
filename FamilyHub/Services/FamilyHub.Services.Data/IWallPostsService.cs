namespace FamilyHub.Services.Data
{
    using System.Collections.Generic;

    public interface IWallPostsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);
    }
}