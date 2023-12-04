using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using Xunit;

public class RatingControllerTests
{
    [Fact]
    public async Task Post_ReturnsCreatedResult_WithValidRating()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<RatingController>>();
        var mockRatingRepository = new Mock<IRatingRepository>();

        var controller = new RatingController(mockLogger.Object, mockRatingRepository.Object);

        var validRating = new Rating
        {
            Id = 1,
            Note = 1,
        };
        // Act
        var result = await controller.Post(validRating);

        // Assert
        var createdAtActionResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
        createdAtActionResult.ActionName.Should().Be(nameof(RatingController.Post));
        createdAtActionResult.Value.Should().BeEquivalentTo(validRating);
        createdAtActionResult.StatusCode.Should().Be(StatusCodes.Status201Created);
    }

   
    [Fact]
    public async Task Put_ReturnsNoContentResult_WithValidRatingId()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<RatingController>>();
        var mockRatingRepository = new Mock<IRatingRepository>();

        var controller = new RatingController(mockLogger.Object, mockRatingRepository.Object);

        int validId = 1;

       
        var validRating = new Rating
        {
            Id = validId,
            // Initialize other properties as needed
        };

        // Act
        var result = await controller.Put(validId, validRating);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Delete_ReturnsNoContentResult_WithValidRatingId()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<RatingController>>();
        var mockRatingRepository = new Mock<IRatingRepository>();

        var controller = new RatingController(mockLogger.Object, mockRatingRepository.Object);

        var validId = 1;

        // Act
        var result = await controller.Delete(validId);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
}
