using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using Xunit;

namespace P7CreateRestApi.Tests.Controllers
{
    public class TradesControllerTests
    {
        [Fact]
        public async Task GetTrades_ReturnsListOfTrades()
        {
            // Arrange
            var mockContext = new Mock<LocalDbContext>();
            var tradeId = 1;
            var setup = mockContext.SetupSequence(x => x.Trades.FindAsync(tradeId));
            setup.ReturnsAsync(new Trade { TradeId = tradeId });
            var controller = new TradesController(mockContext.Object);

            // Act
            var result = await controller.GetTrades();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var trades = (result.Result as OkObjectResult)?.Value as List<Trade>;
            trades.Should().NotBeNull();
            trades.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task GetTrades_ReturnsTradeWithValidId()
        {
            // Arrange
            var mockContext = new Mock<LocalDbContext>();
            var tradeId = 1;
            mockContext.Setup(x => x.Trades.FindAsync(tradeId)).ReturnsAsync(new Trade { TradeId = tradeId });
            var controller = new TradesController(mockContext.Object);

            // Act
            var result = await controller.GetTrade(tradeId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var trade = (result.Result as OkObjectResult)?.Value as Trade;
            trade.Should().NotBeNull();
            trade.TradeId.Should().Be(tradeId);
        }

        [Fact]
        public async Task PutTrade_ReturnsNoContentResult_WithValidTradeId()
        {
            // Arrange
            var mockContext = new Mock<LocalDbContext>();
            var mockLogger = new Mock<ILogger<TradesController>>();
            var tradeId = 1;
            var validTrade = new Trade { TradeId = tradeId };
            mockContext.Setup(x => x.Trades.FindAsync(tradeId)).ReturnsAsync(validTrade);
            var controller = new TradesController(mockLogger.Object, mockContext.Object);

            // Act
            var result = await controller.PutTrade(tradeId, validTrade);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task PostTrade_ReturnsCreatedAtAction()
        {
            // Arrange
            var mockContext = new Mock<LocalDbContext>();
            var mockDbSet = new Mock<DbSet<Trade>>();
            var controller = new TradesController(mockContext.Object);

            // Configure le DbSet
            mockContext.Setup(x => x.Trades).Returns(mockDbSet.Object);

            // Initialisez les propriétés de la trade selon vos besoins
            var tradeToAdd = new Trade { TradeId = 1 /*, autres propriétés */ };

            // Act
            var result = await controller.PostTrade(tradeToAdd);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async Task DeleteTrade_ReturnsNoContentResult_WithValidTradeId()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<TradesController>>();
            var mockTradeRepository = new Mock<ITradeRepository>();
            var mockContext=new Mock<LocalDbContext>();

            var controller = new TradesController(mockLogger.Object, mockTradeRepository.Object, mockContext.Object);

            // Act
            var result = await controller.DeleteTrade(1); // Remplacez 1 par l'ID de la trade que vous souhaitez supprimer

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
