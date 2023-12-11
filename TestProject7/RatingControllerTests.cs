using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using Xunit;

namespace TestProject7
{
    public class RatingControllerTests
    {
        [Fact]
        public async Task Post_ReturnsCreatedAtAction_WhenAddSuccessful()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<RatingController>>();
            var mockRepository = new Mock<IRatingRepository>();
            var controller = new RatingController(mockLogger.Object, mockRepository.Object);

            var rating = new Rating { /* initialize properties */ };

            // Act
            var result = await controller.Post(rating);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var model = Assert.IsType<Rating>(createdAtActionResult.Value);
            mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Rating>()), Times.Once);
        }

        [Fact]
        public async Task Put_ReturnsNoContent_WhenUpdateSuccessful()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<RatingController>>();
            var mockRepository = new Mock<IRatingRepository>();
            var controller = new RatingController(mockLogger.Object, mockRepository.Object);

            var id = 1;
            var rating = new Rating { Id = id, /* initialize other properties */ };

            // Act
            var result = await controller.Put(id, rating);

            // Assert
            Assert.IsType<NoContentResult>(result);
            mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Rating>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenDeleteSuccessful()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<RatingController>>();
            var mockRepository = new Mock<IRatingRepository>();
            var controller = new RatingController(mockLogger.Object, mockRepository.Object);

            var id = 1;

            // Act
            var result = await controller.Delete(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            mockRepository.Verify(repo => repo.DeleteAsync(id), Times.Once);
        }
    }
}
