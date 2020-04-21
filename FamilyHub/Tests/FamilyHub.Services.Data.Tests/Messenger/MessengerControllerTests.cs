namespace FamilyHub.Services.Data.Tests.Messenger
{
    using FamilyHub.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class MessengerControllerTests
    {
        [Fact]
        public void ChatShouldHaveAuthorizedUsersOnlyRestrictionAndShouldReturnView()
            => MyController<MessengerController>
                .Calling(c => c.Chat())
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();
    }
}
