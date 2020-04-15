namespace FamilyHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FamilyHub.Data.Models.Lists;

    public interface IListsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> GetAllByType<T>(ListType type, int? count = null);

        T GetByName<T>(string name);

        T GetById<T>(int id);

        Task<int> CreateAsync(string title, string description, ListType type, string creatorId);

        Task AddItemToList(int listId, string itemText);

        Task ListItemUpdate(int listItemId, string text);

        Task ListItemUpdateDone(int itemId, string userId, DateTime doneTime);

        Task DeleteList(int listId);

        IEnumerable<T> GetAllDeleted<T>(int? count = null);

        Task UnDelete(int listId);
    }
}