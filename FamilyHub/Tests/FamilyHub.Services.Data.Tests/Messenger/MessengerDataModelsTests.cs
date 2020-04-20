namespace FamilyHub.Services.Data.Tests.Messenger
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FamilyHub.Data.Models.Messenger;
    using Xunit;

    public class MessengerDataModelsTests
    {
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
    }
}