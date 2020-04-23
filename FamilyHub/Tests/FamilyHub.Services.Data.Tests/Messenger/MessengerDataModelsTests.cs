namespace FamilyHub.Services.Data.Tests.Messenger
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FamilyHub.Data;
    using FamilyHub.Data.Models.Messenger;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class MessengerDataModelsTests : IDisposable
    {
        private readonly ApplicationDbContext dbContext;

        public MessengerDataModelsTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            this.dbContext = new ApplicationDbContext(options);
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
        }

        [Fact]
        public void ConversationShouldHaveName()
        {
            var conversation = new Conversation
            {
                Name = null,
            };

            var validatorResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(conversation, new ValidationContext(conversation), validatorResults, true);

            Assert.False(actual);
            Assert.Single(validatorResults);
        }

        [Fact]
        public void MessageShouldHaveText()
        {
            var message = new Message
            {
                Text = null,
                UserId = "jjj",
            };

            var validatorResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(message, new ValidationContext(message), validatorResults, true);

            Assert.False(actual);
            Assert.Single(validatorResults);
        }

        [Fact]
        public void MessageShouldHaveUser()
        {
            var message = new Message
            {
                Text = "lll",
                UserId = null,
            };

            var validatorResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(message, new ValidationContext(message), validatorResults, true);

            Assert.False(actual);
            Assert.Single(validatorResults);
        }

        [Fact]
        public void UserConversationShouldHaveUserId()
        {
            var userConversation = new UserConversation
            {
                UserId = null,
                ConversationId = 1,
            };

            Assert.Throws<InvalidOperationException>(
                () => this.dbContext.UserConversations.Add(userConversation));
        }
    }
}
