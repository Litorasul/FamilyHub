namespace FamilyHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FamilyHub.Data.Models.WallPosts;

    public interface IWallPostsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        Task CreateAsync(string creatorId, PostType type, int? assignedEntity, string content);
    }
}