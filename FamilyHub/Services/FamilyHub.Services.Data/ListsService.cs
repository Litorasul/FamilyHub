namespace FamilyHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyHub.Data.Common.Repositories;
    using FamilyHub.Data.Models.Lists;
    using FamilyHub.Data.Models.WallPosts;
    using FamilyHub.Services.Mapping;

    public class ListsService : IListsService
    {
        private readonly IDeletableEntityRepository<List> listRepository;
        private readonly IDeletableEntityRepository<ListItem> listItemRepository;
        private readonly IWallPostsService postsService;
        private readonly IDeletableEntityRepository<Post> postRepository;

        public ListsService(
            IDeletableEntityRepository<List> listRepository,
            IDeletableEntityRepository<ListItem> listItemRepository,
            IWallPostsService postsService,
            IDeletableEntityRepository<Post> postRepository)
        {
            this.listRepository = listRepository;
            this.listItemRepository = listItemRepository;
            this.postsService = postsService;
            this.postRepository = postRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<List> query = this.listRepository.All().OrderBy(l => l.Title);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByType<T>(ListType type, int? count = null)
        {
            IQueryable<List> query = this.listRepository.All().Where(l => l.Type == type).OrderBy(l => l.Title);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetByName<T>(string name)
        {
            var list = this.listRepository
                .All().Where(x => x.Title.Replace(" ", "-") == name)
                .To<T>().FirstOrDefault();

            return list;
        }

        public T GetById<T>(int id)
        {
            var list = this.listRepository
                .All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return list;
        }

        public async Task<int> CreateAsync(string title, string description, ListType type, string creatorId)
        {
            var listToAdd = new List
            {
                Title = title,
                Type = type,
                Description = description,
                CreatorId = creatorId,
                ListItems = new HashSet<ListItem>(),
            };

            await this.listRepository.AddAsync(listToAdd);
            await this.listRepository.SaveChangesAsync();

            await this.postsService.CreateAsync(creatorId, PostType.NewList, listToAdd.Id, null);

            return listToAdd.Id;
        }

        public async Task AddItemToList(int listId, string itemText)
        {
            var listItem = new ListItem
            {
                Text = itemText,
                ListId = listId,
            };

            await this.listItemRepository.AddAsync(listItem);
            await this.listItemRepository.SaveChangesAsync();
        }

        public async Task ListItemUpdate(int listItemId, string text)
        {
            var listItem = this.listItemRepository.All().FirstOrDefault(x => x.Id == listItemId);
            if (listItem != null)
            {
                listItem.Text = text;
            }

            await this.listItemRepository.SaveChangesAsync();
        }

        public async Task ListItemUpdateDone(int itemId, string userId, DateTime doneTime)
        {
            var listItem = this.listItemRepository.All().FirstOrDefault(x => x.Id == itemId);
            if (listItem != null)
            {
                listItem.DoneByUserId = userId;
                listItem.DoneDateTime = doneTime;
            }

            await this.listItemRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllDeleted<T>(int? count = null)
        {
            IQueryable<List> query = this.listRepository.AllWithDeleted()
                .Where(l => l.IsDeleted == true).OrderBy(l => l.Title);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task UnDelete(int listId)
        {
            var list = this.listRepository.AllWithDeleted().FirstOrDefault(e => e.Id == listId);
            if (list != null && list.IsDeleted == true)
            {
                this.listRepository.Undelete(list);
                await this.listRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteList(int listId)
        {
            var list = this.listRepository.All().FirstOrDefault(l => l.Id == listId);
            var post = this.postRepository.All()
                .FirstOrDefault(p => p.PostType == PostType.NewList && p.AssignedEntity == listId);

            if (list != null)
            {
                this.listRepository.Delete(list);
                this.postRepository.Delete(post);

                await this.listRepository.SaveChangesAsync();
                await this.postRepository.SaveChangesAsync();
            }
        }
    }
}
