using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using System.Threading.Tasks;
using Xunit;

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
            var okResult = actionResult.As<OkObjectResult>();
            var returnedRuleName = Assert.IsAssignableFrom<RuleName>(okResult.Value);
            returnedRuleName.Should().BeEquivalentTo(expectedRuleName);

            // Vérifier l'appel au repository GetByIdAsync
            mockRepository.Verify(repo => repo.GetByIdAsync(expectedRuleName.Id), Times.Once);
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

            createdAtActionResult.StatusCode.Should().Be(StatusCodes.Status201Created);
            createdAtActionResult.ActionName.Should().Be(nameof(controller.Get));

            // Vérifier l'appel au repository AddAsync
            mockRepository.Verify(repo => repo.AddAsync(It.IsAny<RuleName>()), Times.Once);
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

            // Act
            var result = await controller.Put(existingRuleName.Id, ruleNameToUpdate);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);

            noContentResult.StatusCode.Should().Be(StatusCodes.Status204NoContent);

            // Vérifier l'appel au repository UpdateAsync
            mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<RuleName>()), Times.Once);
        }

        [Fact]
        public async Task DeleteReturnsNoContent()
        {
            // Arrange
            var mockRepository = new Mock<IRuleNameRepository>();
            var loggerMock = new Mock<ILogger<RuleNameController>>();
            var controller = new RuleNameController(loggerMock.Object, mockRepository.Object);

            var ruleNameIdToDelete = 1;

            // Act
            var result = await controller.Delete(ruleNameIdToDelete);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            noContentResult.StatusCode.Should().Be(StatusCodes.Status204NoContent);

            // Vérifier l'appel au repository DeleteAsync
            mockRepository.Verify(repo => repo.DeleteAsync(ruleNameIdToDelete), Times.Once);
        }
    }
}

