using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;


namespace TestProject7
{
    

        public class TradesControllerTests
    {
        [Fact]
        public async Task Get_ReturnsOkResult_WhenTradeExists()
        {
            // Arrange
            var expectedTrade = new Trade { TradeId = 1, /* ... other properties ... */ };

            var mockRepository = new Mock<ITradeRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(expectedTrade);

            var mockLogger = new Mock<ILogger<TradeController>>();

            var controller = new TradeController(mockLogger.Object, mockRepository.Object);

            // Act
            var result = await controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualTrade = Assert.IsType<Trade>(okResult.Value);
            Assert.Equal(expectedTrade.TradeId, actualTrade.TradeId);

            // Vérifier l'appel au repository GetByIdAsync
            mockRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        }
        [Fact]
        public async Task Post_ReturnsCreatedAtAction_WhenTradeAdded()
        {
            // Arrange
            var newTrade = new Trade { /* ... trade properties ... */ };

            var mockRepository = new Mock<ITradeRepository>();
            var mockLogger = new Mock<ILogger<TradeController>>();
            var controller = new TradeController(mockLogger.Object, mockRepository.Object);

            // Act
            var result = await controller.Post(newTrade);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(StatusCodes.Status201Created, createdAtActionResult.StatusCode);

            // Vérifier l'appel au repository AddAsync
            mockRepository.Verify(repo => repo.AddAsync(newTrade), Times.Once);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenTradeDeleted()
        {
            // Arrange
            var tradeIdToDelete = 1;

            var mockRepository = new Mock<ITradeRepository>();
            mockRepository.Setup(repo => repo.DeleteAsync(tradeIdToDelete)).Returns(Task.CompletedTask);

            var mockLogger = new Mock<ILogger<TradeController>>();
            var controller = new TradeController(mockLogger.Object, mockRepository.Object);

            // Act
            var result = await controller.Delete(tradeIdToDelete);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

            // Vérifier l'appel au repository DeleteAsync
            mockRepository.Verify(repo => repo.DeleteAsync(tradeIdToDelete), Times.Once);
        }
        [Fact]
        public async Task Put_ReturnsNoContent_WhenTradeUpdated()
        {
            // Arrange
            var tradeIdToUpdate = 1;
            var updatedTrade = new Trade { TradeId = tradeIdToUpdate, /* ... updated properties ... */ };

            var mockRepository = new Mock<ITradeRepository>();
            mockRepository.Setup(repo => repo.UpdateAsync(updatedTrade));

            var mockLogger = new Mock<ILogger<TradeController>>();
            var controller = new TradeController(mockLogger.Object, mockRepository.Object);

            // Act
            var result = await controller.Put(tradeIdToUpdate, updatedTrade);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

            // Vérifier l'appel au repository UpdateAsync
            mockRepository.Verify(repo => repo.UpdateAsync(updatedTrade), Times.Once);
        }

    }


}




