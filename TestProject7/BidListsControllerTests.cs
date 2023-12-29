using Dot.Net.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestProject7
{
    public class BidListsControllerTests
    {
        private readonly Mock<IBidListRepository> mockRepository;
        private readonly Mock<ILogger<BidListsController>> mockLogger;

        public BidListsControllerTests()
        {
            mockRepository = new Mock<IBidListRepository>();
            mockLogger = new Mock<ILogger<BidListsController>>();
        }

        [Fact]
        public async Task GetBidLists_ReturnsOkResultWithBidLists()
        {
            // Arrange
            // Arrange
            var controller = new BidListsController(mockLogger.Object, mockRepository.Object);

            // Configure the mock to return a dummy bidList when GetByIdAsync is called
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                          .ReturnsAsync(new BidList { /* initialize with properties */ });

            // Act
            var result = await controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var bidList = Assert.IsType<BidList>(okResult.Value);
            Assert.NotNull(bidList);
        }

        [Fact]
        public async Task PostReturnsBadRequestWhenAddFails()
        {
            // Arrange
            var controller = new BidListsController(mockLogger.Object, mockRepository.Object);
            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<BidList>()))
                .ThrowsAsync(new Exception("Simulated add failure"));

            // Act
            var result = await controller.Post(new BidList());

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task PostReturnsBadRequestWhenModelIsInvalid()
        {
            // Arrange
            var controller = new BidListsController(mockLogger.Object, mockRepository.Object);
            controller.ModelState.AddModelError("Property Name", "Error message");

            // Act
            var result = await controller.Post(new BidList());

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task Put_ReturnsNoContent()
        {
            // Arrange
            var controller = new BidListsController(mockLogger.Object, mockRepository.Object);

            var existingBidList = new BidList { BidListId = 1 };
            var bidListToUpdate = new BidList { BidListId = 1 };

            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<BidList>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await controller.Put(1, bidListToUpdate);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        [Fact]
        public async Task Put_WithMismatchedIds_ReturnsBadRequest()
        {
            // Arrange
            var controller = new BidListsController(mockLogger.Object, mockRepository.Object);

            // Act
            var result = await controller.Put(2, new BidList());

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            // Arrange
            var controller = new BidListsController(mockLogger.Object, mockRepository.Object);

            var bidListIdToDelete = 1;

            mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await controller.Delete(bidListIdToDelete);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        [Fact]
        public async Task DeleteReturnsBadRequestForInvalidId()
        {
            // Arrange
            var controller = new BidListsController(mockLogger.Object, mockRepository.Object);

            int invalidId = -1; // Use an invalid ID that will trigger a BadRequest

            // Act
            var result = await controller.Delete(invalidId);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }
    }
}

