namespace FamilyHub.Services.Data.Tests.Messenger
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyHub.Data;
    using FamilyHub.Data.Common.Repositories;
    using FamilyHub.Data.Models;
    using FamilyHub.Data.Models.Messenger;
    using FamilyHub.Data.Repositories;
    using FamilyHub.Services.Mapping;
    using FamilyHub.Web.ViewModels.Tests;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class MessengerServiceTests : IDisposable
    {
        private readonly IDeletableEntityRepository<Conversation> conversationRepository;
        private readonly IDeletableEntityRepository<Message> messageRepository;
        private readonly ApplicationDbContext dbContext;

        public MessengerServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            this.dbContext = new ApplicationDbContext(options);

            this.messageRepository = new EfDeletableEntityRepository<Message>(this.dbContext);
            this.conversationRepository = new EfDeletableEntityRepository<Conversation>(this.dbContext);

            AutoMapperConfig.RegisterMappings(typeof(TestConversationViewModel).Assembly);
            AutoMapperConfig.RegisterMappings(typeof(TestMessageViewModel).Assembly);
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
            this.messageRepository.Dispose();
            this.conversationRepository.Dispose();
        }

        [Fact]
        public async Task AddMessageShouldAddMessage()
        {
            var messages = new List<Message>();

            var repository = new Mock<IDeletableEntityRepository<Message>>();
            repository
                .Setup(r => r.AddAsync(It.IsAny<Message>()))
                .Callback((Message m) => messages.Add(m));

            var service = new MessengerService(this.conversationRepository, repository.Object);
            await service.AddMessage<TestMessageViewModel>("aaa", "bbb");

            Assert.Single(messages);
            Assert.Equal("aaa", messages[0].UserId);
            Assert.Equal("bbb", messages[0].Text);
        }

        [Fact]
        public async Task AddMessageShouldReturnTheRightMessage()
        {
            var service = new MessengerService(this.conversationRepository, this.messageRepository);
            var message = await service.AddMessage<TestMessageViewModel>("ccc", "ddd");

            Assert.Equal("ccc", message.UserId);
            Assert.Equal("ddd", message.Text);
        }

        [Fact]
        public async Task GetAllMessagesShouldReturnAllMessages()
        {
            await this.PopulateMessages();
            var service = new MessengerService(this.conversationRepository, this.messageRepository);

            List<TestMessageViewModel> messages = service
                .GetAllMessages<TestMessageViewModel>()
                .ToList();

            Assert.Equal(2, messages.Count);
            Assert.Equal("bbbb", messages[1].Text);
            Assert.Equal("cccc", messages[0].UserId);
        }

        [Fact]
        public async Task GetAllConversationShouldReturnAllConversationPerUser()
        {
            await this.PopulateConversation();

            var service = new MessengerService(this.conversationRepository, this.messageRepository);

            List<TestConversationViewModel> conversations = service
                .GetAllConversation<TestConversationViewModel>("ddd")
                .ToList();

            Assert.Single(conversations);
            Assert.Equal("bbb", conversations[0].Name);
        }

        [Fact]
        public async Task GetConversationNameByIdShouldReturnTheRightName()
        {
            await this.PopulateConversation();

            var service = new MessengerService(this.conversationRepository, this.messageRepository);

            string conversationName = service.GetConversationNameById(3);

            Assert.Equal("ccc", conversationName);
        }

        private async Task PopulateConversation()
        {
            var conversationOne = new Conversation
            {
                Id = 1,
                Name = "aaa",
            };

            var conversationTwo = new Conversation
            {
                Id = 2,
                Name = "bbb",
            };

            var conversationThree = new Conversation
            {
                Id = 3,
                Name = "ccc",
            };

            var user = new ApplicationUser
            {
                Id = "ddd",
                UserName = "eee",
                Email = "fff",
                EmailConfirmed = true,
            };

            this.dbContext.Conversations.Add(conversationOne);
            this.dbContext.Conversations.Add(conversationTwo);
            this.dbContext.Conversations.Add(conversationThree);
            this.dbContext.Users.Add(user);
            await this.dbContext.SaveChangesAsync();

            var userConv = new UserConversation
            {
                UserId = "ddd",
                ConversationId = 2,
            };

            this.dbContext.UserConversations.Add(userConv);
            await this.dbContext.SaveChangesAsync();
        }

        private async Task PopulateMessages()
        {
            var messageOne = new Message
            {
                UserId = "aaaa",
                Text = "bbbb",
                CreatedOn = DateTime.Now,
            };

            var messageTwo = new Message
            {
                UserId = "cccc",
                Text = "dddd",
                CreatedOn = DateTime.UtcNow,
            };

            this.dbContext.Messages.Add(messageOne);
            this.dbContext.Messages.Add(messageTwo);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
