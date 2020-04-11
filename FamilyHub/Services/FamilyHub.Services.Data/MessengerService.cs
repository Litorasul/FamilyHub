namespace FamilyHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyHub.Data.Common.Repositories;
    using FamilyHub.Data.Models;
    using FamilyHub.Data.Models.Messenger;
    using FamilyHub.Services.Mapping;

    public class MessengerService : IMessengerService
    {
        private readonly IDeletableEntityRepository<Conversation> conversationRepository;
        private readonly IDeletableEntityRepository<Message> messageRepository;

        public MessengerService(
            IDeletableEntityRepository<Conversation> conversationRepository,
            IDeletableEntityRepository<Message> messageRepository)
        {
            this.conversationRepository = conversationRepository;
            this.messageRepository = messageRepository;
        }

        public IEnumerable<T> GetAllConversation<T>(string userId, int? count = null)
        {
            IQueryable<Conversation> query = this.conversationRepository.All()
                .Where(c => c.Users.Any(u => u.UserId == userId))
                .OrderByDescending(c => c.CreatedOn);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            var queryList = query.To<T>().ToList();
            return queryList;
        }

        public IEnumerable<T> GetAllMessages<T>()
        {
            IQueryable<Message> query = this.messageRepository.All().OrderBy(m => m.CreatedOn);

            return query.To<T>().ToList();
        }

        public string GetConversationNameById(int conversationId)
        {
            string name = this.conversationRepository.All()
                .Where(c => c.Id == conversationId)
                .Select(x => x.Name)
                .FirstOrDefault();

            return name;
        }

        public async Task<T> AddMessage<T>(string userId, string text)
        {
            var message = new Message
            {
                UserId = userId,
                Text = text,
            };

            await this.messageRepository.AddAsync(message);
            await this.messageRepository.SaveChangesAsync();

            var messageTo = this.messageRepository.All().Where(x => x.Id == message.Id).To<T>().FirstOrDefault();

            return messageTo;
        }
    }
}
