namespace FamilyHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using FamilyHub.Data.Common.Repositories;
    using FamilyHub.Data.Models;
    using FamilyHub.Data.Models.Messenger;
    using FamilyHub.Services.Mapping;

    public class MessengerService : IMessengerService
    {
        private readonly IDeletableEntityRepository<Conversation> conversationRepository;
        private readonly IRepository<UserConversation> userConversationRepository;

        public MessengerService(
            IDeletableEntityRepository<Conversation> conversationRepository,
            IRepository<UserConversation> userConversationRepository)
        {
            this.conversationRepository = conversationRepository;
            this.userConversationRepository = userConversationRepository;
        }

        public IEnumerable<T> GetAllConversation<T>(string userId, int? count = null)
        {
            var userConversations = this.userConversationRepository.All().Where(x => x.UserId == userId);
            IQueryable<Conversation> query = this.conversationRepository.All()
                .Where(c => c.Users == userConversations)
                .OrderBy(c => c.CreatedOn);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}