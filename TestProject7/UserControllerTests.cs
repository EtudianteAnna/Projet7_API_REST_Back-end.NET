using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;
using P7CreateRestApi.Repositories;
using P7CreateRestApi.Domain;
using Microsoft.AspNetCore.Http;

namespace TestProject7
{
    public class UserControllerTests
    {
        [Fact]
        public async Task AddUserReturnsOkResult()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var mockLogger = new Mock<ILogger<UserController>>();

            var controller = new UserController(mockLogger.Object, mockRepository.Object);

            var userToAdd = new User { /* set properties */ };

            // Act
            var result = await controller.AddUser(userToAdd);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        
        public async Task AddUserReturnsNoResult()
        {

            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var mockLogger = new Mock<ILogger<UserController>>();

            var controller = new UserController(mockLogger.Object, mockRepository.Object);

            var userToAdd = new User { /* set properties */ };

            // Act
            var result = await controller.AddUser(userToAdd);

            // Assert
            Assert.IsType<OkResult>(result);

        }

        [Fact]
    public async Task Validate_ReturnsOkResult_WhenModelStateIsValide()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var mockLogger = new Mock<ILogger<UserController>>();

        var controller = new UserController(mockLogger.Object, mockRepository.Object);
        controller.ModelState.AddModelError("PropertyName", "Error message");

        var userToValidate = new User { /* set properties */ };

        // Act
        var result = await controller.Validate(userToValidate);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
    [Fact]
        public async Task Validate_ReturnsBadRequestResultWhenModelStateIsInvalid()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var mockLogger = new Mock<ILogger<UserController>>();

        var controller = new UserController(mockLogger.Object, mockRepository.Object);
        controller.ModelState.AddModelError("PropertyName", "Error message");

        var userToValidate = new User { /* set properties */ };

        // Act
        var result = await controller.Validate(userToValidate);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

        [Fact]
        public async Task DeleteUser_ReturnsOkResult()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var mockLogger = new Mock<ILogger<UserController>>();

            var controller = new UserController(mockLogger.Object, mockRepository.Object);

            var userIdToDelete = 1;

            // Act
            var result = await controller.DeleteUser(userIdToDelete);

            // Assert
            Assert.IsType<OkResult>(result);
        }
        [Fact]
        public async Task GetAllUserArticles_ReturnsOkResultWithUsers()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var mockLogger = new Mock<ILogger<UserController>>();
            var users= new List <User> (); 
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

            var controller = new UserController(mockLogger.Object, mockRepository.Object);

            // Act
            var result = await controller.GetAllUserArticles();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUsers = Assert.IsAssignableFrom<List<User>>(okResult.Value);
            Assert.Equal(users.Count, returnedUsers.Count);
        }

        [Fact]
        public async Task GetAllUserArticles_ReturnsOkResultWithoutUsers()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var mockLogger = new Mock<ILogger<UserController>>();

            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<User>()); // Simulate an empty list

            var controller = new UserController(mockLogger.Object, mockRepository.Object);

            // Act
            var result = await controller.GetAllUserArticles();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUsers = Assert.IsAssignableFrom<List<User>>(okResult.Value);
            Assert.Empty(returnedUsers);
        }

    }


}
