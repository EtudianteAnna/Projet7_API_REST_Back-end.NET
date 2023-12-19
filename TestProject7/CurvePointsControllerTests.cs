using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using P7CreateRestApi.Repositories;

namespace TestProject7
{
    public class CurvePointsControllerTests
    {
        [Fact]
        public async Task GetCurvePointReturnsCurvePoint()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<CurvePointsController>>();
            var mockRepository = new Mock<ICurvePointRepository>();
            var controller = new CurvePointsController(mockLogger.Object, mockRepository.Object);

            int id = 1;
            var expectedCurvePoint = new CurvePoints { Id = id, /* initialize other properties */ };
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(expectedCurvePoint);

            // Act
            var result = await controller.GetCurvePoint(id);

            // Assert
            var curvePointResult = Assert.IsType<ActionResult<CurvePoints>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(curvePointResult.Result);
            var model = Assert.IsType<CurvePoints>(okObjectResult.Value);
            Assert.Equal(expectedCurvePoint.Id, model.Id);

            // Ensure that the repository GetByIdAsync method was called with the correct ID
            mockRepository.Verify(repo => repo.GetByIdAsync(id), Times.Once);
        }
        [Fact]
        public async Task PutCurvePointWithValidIdReturnsNoContent()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<CurvePointsController>>();
            var mockRepository = new Mock<ICurvePointRepository>();
            var controller = new CurvePointsController(mockLogger.Object, mockRepository.Object);

            int id = 1;
            var existingCurvePoint = new CurvePoints { Id = id, /* initialize other properties */ };
            var updatedCurvePoint = new CurvePoints { Id = id, /* updated properties */ };

            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingCurvePoint);

            // Act
            var result = await controller.PutCurvePoint(id, updatedCurvePoint);

            // Assert
            Assert.IsType<NoContentResult>(result);

            // Ensure that the repository GetByIdAsync and UpdateAsync methods were called with the correct ID and updatedCurvePoint
            mockRepository.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            mockRepository.Verify(repo => repo.UpdateAsync(updatedCurvePoint), Times.Once);
        }
        [Fact]
        public async Task PostCurvePoint_ReturnsCreatedAtAction()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<CurvePointsController>>();
            var mockRepository = new Mock<ICurvePointRepository>();
            var controller = new CurvePointsController(mockLogger.Object, mockRepository.Object);

            var curvePoint = new CurvePoints { Id = 1, /* initialize other properties */ };

            // Act
            var result = await controller.PostCurvePoint(curvePoint);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var model = Assert.IsType<CurvePoints>(createdAtActionResult.Value);
            Assert.Equal(curvePoint.Id, model.Id);

            // Ensure that the repository AddAsync method was called
            mockRepository.Verify(repo => repo.AddAsync(It.IsAny<CurvePoints>()), Times.Once);
        }
        [Fact]
        public async Task DeleteCurvePointWithValidIdReturnsNoContent()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<CurvePointsController>>();
            var mockRepository = new Mock<ICurvePointRepository>();
            var controller = new CurvePointsController(mockLogger.Object, mockRepository.Object);

            int id = 1;
            var existingCurvePoint = new CurvePoints { Id = id, /* initialize other properties */ };
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(existingCurvePoint);

            // Act
            var result = await controller.DeleteCurvePoint(id);

            // Assert
            Assert.IsType<NoContentResult>(result);

            // Ensure that the repository GetByIdAsync and DeleteAsync methods were called with the correct ID
            mockRepository.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            mockRepository.Verify(repo => repo.DeleteAsync(id), Times.Once);
        }
        [Fact]
        public async Task DeleteCurvePointWithInvalidIdReturnsNotFound()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<CurvePointsController>>();
            var mockRepository = new Mock<ICurvePointRepository>();
            var controller = new CurvePointsController(mockLogger.Object, mockRepository.Object);

            int id = 1;
            mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(()=> null);

            // Act
            var result = await controller.DeleteCurvePoint(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);

            // Ensure that the repository GetByIdAsync and DeleteAsync methods were called with the correct ID
            mockRepository.Verify(repo => repo.GetByIdAsync(id), Times.Once);
            mockRepository.Verify(repo => repo.DeleteAsync(id), Times.Never);
        }
    }
}

