namespace FamilyHub.Services.Data.Tests.WallPosts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FamilyHub.Data.Common.Repositories;
    using FamilyHub.Data.Models.WallPosts;
    using Moq;
    using Xunit;

    public class CommentsServiceTests
    {
        [Fact]
        public async Task CreateAsyncShouldCreateComment()
        {
            var comments = new List<Comment>();
            var repository = new Mock<IDeletableEntityRepository<Comment>>();
            repository
                .Setup(r => r.AddAsync(It.IsAny<Comment>()))
                .Callback((Comment c) => comments.Add(c));

            var service = new CommentsService(repository.Object);
            await service.CreateAsync("aaa", 1, "Test");

            Assert.Equal("Test", comments[0].Text);
            Assert.Equal("aaa", comments[0].UserId);
            Assert.Equal(1, comments[0].PostId);
        }
    }
}
