using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;

namespace TestProject7
{
    public class RuleNameControllerTests
    {
        [Fact]
        public async Task Get_ReturnsCorrectRuleName()
        {
            // Arrange
            var mockRepository = new Mock<IRuleNameRepository>();
            var expectedRuleName = new RuleName { Id = 1, /* other properties */ };
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                          .ReturnsAsync(expectedRuleName);

            var loggerMock = new Mock<ILogger<RuleNameController>>();

            var controller = new RuleNameController(loggerMock.Object, mockRepository.Object);

            // Act
            var actionResult = await controller.Get(expectedRuleName.Id);

            // Assert
            // Utiliser la méthode As<T> de Microsoft.AspNetCore.Mvc pour obtenir le résultat
            var okResult = actionResult.As<OkObjectResult>();
            var returnedRuleName = Assert.IsAssignableFrom<RuleName>(okResult.Value);
            Assert.Equal(expectedRuleName.Id, returnedRuleName.Id);
        }
        [Fact]
        public async Task Post_ReturnsCreatedAtAction()
        {
            // Arrange
            var mockRepository = new Mock<IRuleNameRepository>();
            var loggerMock = new Mock<ILogger<RuleNameController>>();

            var controller = new RuleNameController(loggerMock.Object, mockRepository.Object);

            var ruleNameToCreate = new RuleName { /* set properties */ };

            // Act
            var result = await controller.Post(ruleNameToCreate);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(StatusCodes.Status201Created, createdAtActionResult.StatusCode);
            Assert.Equal(nameof(controller.Get), createdAtActionResult.ActionName);
            // Add more assertions based on your specific requirements
        }

        [Fact]
        public async Task Put_ReturnsNoContent()
        {
            // Arrange
            var mockRepository = new Mock<IRuleNameRepository>();
            var loggerMock = new Mock<ILogger<RuleNameController>>();

            var controller = new RuleNameController(loggerMock.Object, mockRepository.Object);

            var existingRuleName = new RuleName { Id = 1, /* other properties */ };
            var ruleNameToUpdate = new RuleName { Id = 1, /* updated properties */ };

            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<RuleName>()))
                .Callback<RuleName>(updatedRuleName =>
                {
                    // Verify that the UpdateAsync method was called with the correct parameters
                    Assert.Equal(ruleNameToUpdate.Id, updatedRuleName.Id);

                })
                .Returns(Task.CompletedTask);

            // Act
            var result = await controller.Put(1, ruleNameToUpdate);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);

            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }
            [Fact]
        public async Task DeleteReturnsNoContent()
        {
            // Arrange
            var mockRepository = new Mock<IRuleNameRepository>();
            var loggerMock = new Mock<ILogger<RuleNameController>>();

            var controller = new RuleNameController(loggerMock.Object, mockRepository.Object);

            var ruleNameIdToDelete = 1;

            mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
                .Callback<int>(id =>
                {
                    // Verify that the DeleteAsync method was called with the correct parameter
                    Assert.Equal(ruleNameIdToDelete, id);

                })
                .Returns(Task.CompletedTask);

            // Act
            var result = await controller.Delete(ruleNameIdToDelete);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }
    }
}

