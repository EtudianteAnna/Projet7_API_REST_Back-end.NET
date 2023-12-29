using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;
using P7CreateRestApi.Repositories;
using P7CreateRestApi.Domain;
using Microsoft.AspNetCore.Http;

namespace TestProject7
{
    public class UserControllerTests
    {
        [Fact]
        public async Task AddUserReturnsOkResult()
        {
            // Arrange
            var mockRepository = new Mock<IBidListRepository>();
            var controller = new UserController(mockRepository.Object);

            var expectedBidLists = new List<BidList> { /* initialize your expected list */ };
            mockRepository.Setup(repo => repo.GetBidListsAsync()).ReturnsAsync(expectedBidLists);

            // Act
            var result = await controller.GetUserLists();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualBidLists = Assert.IsAssignableFrom<IEnumerable<BidList>>(okResult.Value);
            Assert.Equal(expectedBidLists, actualBidLists);
        }

        [Fact]
        public async Task GetUserListReturnsOkResultWithBidList()
        {

            // Arrange
            var mockRepository = new Mock<IBidListRepository>();
            var controller = new UserController(mockRepository.Object);

            var expectedBidList = new BidList { /* initialize your expected BidList */ };
            var bidListId = 1;
            mockRepository.Setup(repo => repo.GetByIdAsync(bidListId)).ReturnsAsync(expectedBidList);

            var userToAdd = new User { /* set properties */ };

            // Act
            var result = await controller.GetUserList(bidListId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualBidList = Assert.IsType<BidList>(okResult.Value);
            Assert.Equal(expectedBidList, actualBidList);
        }

        [Fact]
        public async Task PutUserListReturnsNoContent()
        {
            // Arrange
            var mockRepository = new Mock<IBidListRepository>();
            var controller = new UserController(mockRepository.Object);

            var bidListId = 1;
            var bidList = new BidList { BidListId = bidListId, /* initialize other properties */ };

            // Act
            var result = await controller.PutUserList(bidListId, bidList);

            // Assert
            Assert.IsType<NoContentResult>(result);
            mockRepository.Verify(repo => repo.UpdateAsync(bidList), Times.Once);
        }

        [Fact]
        public async Task PostUserListReturnsCreatedAtAction()
        {
            // Arrange
            var mockRepository = new Mock<IBidListRepository>();
            var controller = new UserController(mockRepository.Object);

            var bidList = new BidList { /* initialize your BidList */ };

            // Act
            var result = await controller.GetAllUserArticles();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUsers = Assert.IsAssignableFrom<List<User>>(okResult.Value);
            Assert.Equal(users.Count, returnedUsers.Count);
        }
    

        [Fact]
        public async Task DeleteUserListReturnsNoContent()
        {
            // Arrange
            var mockRepository = new Mock<IBidListRepository>();
            var controller = new UserController(mockRepository.Object);

            var bidListId = 1;

            // Act
            var result = await controller.GetAllUserArticles();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUsers = Assert.IsAssignableFrom<List<User>>(okResult.Value);
            Assert.Empty(returnedUsers);
        }

    }


}
