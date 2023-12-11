using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace TestProject7
{
    public class UserControllerTests
    {
        [Fact]
        public async Task GetUserListsReturnsOkResultWithListOfBidLists()
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
            var result = await controller.PostUserList(bidList);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var model = Assert.IsType<BidList>(createdAtActionResult.Value);
            Assert.Equal(bidList.BidListId, model.BidListId);
            mockRepository.Verify(repo => repo.AddAsync(bidList), Times.Once);
        }

        [Fact]
        public async Task DeleteUserListReturnsNoContent()
        {
            // Arrange
            var mockRepository = new Mock<IBidListRepository>();
            var controller = new UserController(mockRepository.Object);

            var bidListId = 1;

            // Act
            var result = await controller.DeleteUserList(bidListId);

            // Assert
            Assert.IsType<NoContentResult>(result);
            mockRepository.Verify(repo => repo.DeleteAsync(bidListId), Times.Once);
        }
    }
}
