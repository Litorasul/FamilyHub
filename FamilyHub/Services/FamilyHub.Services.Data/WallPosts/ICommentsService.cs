namespace FamilyHub.Services.Data.WallPosts
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task CreateAsync(string creatorId, int postId, string text);
    }
}