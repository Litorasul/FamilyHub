using System.Threading.Tasks;

namespace FamilyHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using FamilyHub.Data.Common.Repositories;
    using FamilyHub.Data.Models.Lists;
    using FamilyHub.Services.Mapping;

    public class ListsService : IListsService
    {
        private readonly IDeletableEntityRepository<List> listRepository;
        private readonly IDeletableEntityRepository<ListItem> listItemRepository;

        public ListsService(IDeletableEntityRepository<List> listRepository, IDeletableEntityRepository<ListItem> listItemRepository)
        {
            this.listRepository = listRepository;
            this.listItemRepository = listItemRepository;
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
    }
}