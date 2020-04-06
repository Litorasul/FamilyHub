namespace FamilyHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyHub.Data.Common.Repositories;
    using FamilyHub.Data.Models.WallPosts;
    using FamilyHub.Services.Mapping;

    public class WallPostsService : IWallPostsService
    {
        private readonly IDeletableEntityRepository<Post> postRepository;

        public WallPostsService(IDeletableEntityRepository<Post> postRepository)
        {
            this.postRepository = postRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Post> query = this.postRepository.All().OrderByDescending(e => e.CreatedOn);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task CreateAsync(string creatorId, PostType type, int? assignedEntity, string content)
        {
            var post = new Post
            {
                UserId = creatorId,
                PostType = type,
                AssignedEntity = assignedEntity,
                Content = content,
            };

            await this.postRepository.AddAsync(post);
            await this.postRepository.SaveChangesAsync();
        }
    }
}