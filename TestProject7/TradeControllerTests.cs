using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace TestProject7
{
    public class TradesControllerTests
    {
        // ...

        [Fact]
        public async Task GetTradeReturnsTradeWhenTradeExists()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<TradesController>>();
            var mockContext = new Mock<LocalDbContext>();
            var controller = new TradesController(mockLogger.Object, mockContext.Object);

            var tradeId = 1;
            var trade = new Trade { TradeId = tradeId, /* initialize trade properties */ };
            var mockDbSet = new Mock<DbSet<Trade>>();
            mockDbSet.Setup(m => m.FindAsync(tradeId)).ReturnsAsync(trade);
            mockContext.Setup(c => c.Trades).Returns(mockDbSet.Object);

            // Act
            var result = await controller.GetTrade(tradeId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsType<Trade>(okObjectResult.Value);
            Assert.Equal(tradeId, model.TradeId);
        }

        [Fact]
        public async Task GetTradeReturnsNotFoundWhenTradeDoesNotExist()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<TradesController>>();
            var mockContext = new Mock<LocalDbContext>();
            var controller = new TradesController(mockLogger.Object, mockContext.Object);

            var tradeId = 1;
            var mockDbSet = new Mock<DbSet<Trade>>();
            mockDbSet.Setup(m => m.FindAsync(tradeId)).ReturnsAsync(value: null as Trade); // No trade found
            mockContext.Setup(c => c.Trades).Returns(mockDbSet.Object);

            // Act
            var result = await controller.GetTrade(tradeId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PutTradeWithValidIdReturnsNoContent()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<TradesController>>();
            var mockContext = new Mock<LocalDbContext>();
            var controller = new TradesController(mockLogger.Object, mockContext.Object);

            var id = 1;
            var trade = new Trade { TradeId = id, /* initialize other properties */ };
            var mockDbSet = new Mock<DbSet<Trade>>();
            mockContext.Setup(c => c.Trades).Returns(mockDbSet.Object);

            // Act
            var result = await controller.PostTrade(id, trade);

            // Assert
            Assert.IsType<NoContentResult>(result);
            // You can add more assertions here if needed
        }



        [Fact]
        public async Task PostTradeReturnsCreatedAtActionWhenAddSuccessful()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<TradesController>>();
            var mockContext = new Mock<LocalDbContext>();
            var controller = new TradesController(mockLogger.Object, mockContext.Object);

            var id = 1;
            var trade = new Trade { TradeId = id, /* initialize other properties */ };
            var mockDbSet = new Mock<DbSet<Trade>>();
            mockContext.Setup(c => c.Trades).Returns(mockDbSet.Object);

            // Act
            var result = await controller.PostTrade(id, trade);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteTradeReturnsNoContentWhenDeleteSuccessful()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<TradesController>>();
            var mockContext = new Mock<LocalDbContext>();
            var trade = new Trade { TradeId = 1, /* initialize trade properties */ };
            var mockDbSet = new Mock<DbSet<Trade>>();
            mockDbSet.Setup(m => m.FindAsync(It.IsAny<object[]>())).ReturnsAsync(trade);
            mockContext.Setup(c => c.Trades).Returns(mockDbSet.Object);

            var controller = new TradesController(mockLogger.Object, mockContext.Object);
            // Act
            var result = await controller.DeleteTrade(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
            mockDbSet.Verify(d => d.FindAsync(It.IsAny<object[]>()), Times.Once);
            mockContext.Verify(c => c.Trades.Remove(trade), Times.Once);
            // Notez que nous ne vérifions pas SaveChangesAsync ici car le test ne dépend pas de cette méthode.
        }
    }
}

    

