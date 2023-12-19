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
            // ... assert other properties as needed ...
        }

        [Fact]
        public async Task Get_ReturnsNotFoundResult_WhenTradeDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<ITradeRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Trade)null);

            var mockLogger = new Mock<ILogger<TradeController>>();

            var controller = new TradeController(mockLogger.Object, mockRepository.Object);

            // Act
            var result = await controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Similar tests can be written for other actions in TradeController
    }
}

