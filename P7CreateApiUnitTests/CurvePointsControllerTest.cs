using Moq;
using Microsoft.AspNetCore.Mvc;
using Assert = Xunit.Assert;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Repositories;
using Xunit;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Data;

namespace P7CreateApiUnitTests
{
    public class CurvePointsControllerTest
    {

        [Fact]
        public async Task GetCurvePoint_ReturnsOkResult_WithValidId()
        {
            // Arrange
            var mockContext = new Mock<LocalDbContext>();
            var mockRepository = new Mock<ICurvePointRepository>();
            var controller = new CurvePointsController(mockContext.Object, mockRepository.Object);


            int validId = 1;
            var fakeCurvePoint = new CurvePoints { Id = validId /* autres propriétés */ };
            mockRepository.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(fakeCurvePoint);

            // Act
            var result = await controller.GetCurvePoint(validId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var curvePoint = Assert.IsAssignableFrom<CurvePoints>(okResult.Value);
            Assert.Equal(fakeCurvePoint.Id, curvePoint.Id);
        }

        [Fact]
        public async Task PutCurvePoint_ReturnsNoContent_WithValidIdAndMatchingCurvePointId()
        {
            // Arrange
            var mockContext = new Mock<LocalDbContext>();
            var mockRepository = new Mock<ICurvePointRepository>();
            var controller = new CurvePointsController(mockContext.Object, mockRepository.Object);

            int validId = 1;
            var fakeCurvePoint = new CurvePoints { Id = validId /* autres propriétés */ };
            mockRepository.Setup(repo => repo.UpdateAsync(fakeCurvePoint)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.PutCurvePoint(validId, fakeCurvePoint);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PostCurvePoint_ReturnsCreatedAtAction_WithValidCurvePoint()
        {
            // Arrange
            var mockContext = new Mock<LocalDbContext>();
            var mockRepository = new Mock<ICurvePointRepository>();
            var controller = new CurvePointsController(mockContext.Object, mockRepository.Object);
            var fakeCurvePoint = new CurvePoints { /* autres propriétés */ };

            mockRepository.Setup(repo => repo.AddAsync(fakeCurvePoint)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.PostCurvePoint(fakeCurvePoint);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetCurvePoint", createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task DeleteCurvePoint_ReturnsNoContent_WithValidIdAndExistingCurvePoint()
        {
            // Arrange

            var mockContext = new Mock<LocalDbContext>();
            var mockRepository = new Mock<ICurvePointRepository>();
            var controller = new CurvePointsController(mockContext.Object, mockRepository.Object);
            int validId = 1;
            var fakeCurvePoint = new CurvePoints { Id = validId /* autres propriétés */ };
            mockRepository.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(fakeCurvePoint);
            mockRepository.Setup(repo => repo.DeleteAsync(validId)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.DeleteCurvePoint(validId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCurvePoint_ReturnsNotFound_WithNonExistingCurvePoint()
        {
            // Arrange

            var mockContext = new Mock<LocalDbContext>();
            var mockRepository = new Mock<ICurvePointRepository>();
            var controller = new CurvePointsController(mockContext.Object, mockRepository.Object);

            int nonExistingId = 999; // Utilisez un ID qui n'existe pas dans votre jeu de données simulé
            mockRepository.Setup(repo => repo.GetByIdAsync(nonExistingId)).ReturnsAsync((CurvePoints)null);

            // Act
            var result = await controller.DeleteCurvePoint(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}