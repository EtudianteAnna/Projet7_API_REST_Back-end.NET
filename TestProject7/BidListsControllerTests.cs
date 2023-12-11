using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;

namespace TestProject7
{   
    public class BidListsControllerTests
    {
        private readonly Mock<IBidListRepository> mockRepository;
        private readonly Mock<ILogger<BidListsController>> mockLogger;

        public BidListsControllerTests()

        {
            mockRepository = new Mock<IBidListRepository>();
            Mock<ILogger<BidListsController>> mock = new();

            mockLogger = mock;
        }

        [Fact]
        public async Task GetBidLists_ReturnsOkResultWithBidLists()
        {
            // Arrange
            var mockRepository = new Mock<IBidListRepository>();
            var controller = new BidListsController(mockRepository.Object);

            var bidLists = new List<BidList> { new BidList { BidListId = 1, /* other properties */ } };
            mockRepository.Setup(repo => repo.GetBidListsAsync()).ReturnsAsync(bidLists);

            // Act
            var result = await controller.GetBidLists();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsType<List<BidList>>(okResult.Value);
            Assert.Single(model);
            Assert.Equal(bidLists, model);
        }

        [Fact]
        public async Task GetBidList_ReturnsOkResultWithBidList()
        {
            // Arrange
            var mockRepository = new Mock<IBidListRepository>();
            var controller = new BidListsController(mockRepository.Object);

            var bidList = new BidList { BidListId = 1, /* other properties */ };
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(bidList);

            // Act
            var result = await controller.GetBidList(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsType<BidList>(okResult.Value);
            Assert.Equal(bidList, model);
        }

        [Fact]
        public async Task PutBidList_ReturnsNoContentResult()
        {
            // Arrange
            var mockRepository = new Mock<IBidListRepository>();
            var controller = new BidListsController(mockRepository.Object);

            var bidList = new BidList { BidListId = 1, /* other properties */ };

            // Act
            var result = await controller.PutBidList(1, bidList);

            // Assert
            Assert.IsType<NoContentResult>(result);
            mockRepository.Verify(repo => repo.UpdateAsync(bidList), Times.Once);
        }

        [Fact]
        public async Task PostBidList_ReturnsCreatedAtActionResult()
        {
            //Arrange
            // Correction en utilisant la propriété BidListId
            var mockLogger = new Mock<ILogger<BidListsController>>();
            var mockRepository = new Mock<IBidListRepository>();
            var controller = new BidListsController(mockLogger.Object, mockRepository.Object);


            // Act
            ActionResult<BidList> actionResult = await controller.PostBidList(new BidList { BidListId = 1, });
            var result = actionResult;

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var model = Assert.IsType<BidList>(createdAtActionResult.Value);
            
        }

        [Fact]
        public async Task DeleteBidList_ReturnsNoContentResult()
        {
            // Arrange
            var mockRepository = new Mock<IBidListRepository>();
            var controller = new BidListsController(mockRepository.Object);

            var bidList = new BidList { BidListId = 1, /* other properties */ };
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(bidList);

            // Act
            var result = await controller.DeleteBidList(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
            mockRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }
    }
}

