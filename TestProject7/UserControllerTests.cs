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
            var loggerMock = new Mock<ILogger<UserController>>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var userManagerMock = new Mock<TestUserManager>();

            var controller = new UserController(loggerMock.Object, userRepositoryMock.Object, userManagerMock.Object);

            var user = new User
            {
                Id = "someUserId", // Set a valid user ID
                                   // Set other properties as needed for testing
            };

            // Act
            var result = await controller.AddUser(user);

            // Assert
            Assert.IsType<OkResult>(result);

            // Optionally, you can add more assertions based on the specific behavior you expect
            // For example, you might want to verify that the AddAsync method was called on the UserRepository
            userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Once);
        }
        [Fact]
        public async Task ValidateReturnsOkResultWhenModelStateIsValid()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<UserController>>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var userManagerMock = new Mock<TestUserManager>();

            var controller = new UserController(loggerMock.Object, userRepositoryMock.Object, userManagerMock.Object);

            var validUser = new User
            {
                Id = "someUserId", // Set a valid user ID
                                   // Set other properties as needed for testing
            };

            // Act
            var result = await controller.Validate(validUser);

            // Assert
            Assert.IsType<OkResult>(result);
            userRepositoryMock.Verify(repo => repo.AddAsync(validUser), Times.Once);
        }

        [Fact]
        public async Task ValidateReturnsBadRequestResultWhenModelStateIsInvalid()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<UserController>>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var userManagerMock = new Mock<TestUserManager>();

            var controller = new UserController(loggerMock.Object, userRepositoryMock.Object, userManagerMock.Object);

            // Simulate a model state error by adding an error to the ModelState
            controller.ModelState.AddModelError("PropertyName", "Error message");

            var invalidUser = new User
            {
                Id = "someUserId", // Set a valid user ID
                                   // Set other properties as needed for testing
            };

            // Act
            var result = await controller.Validate(invalidUser);

            // Assert
            Assert.IsType<BadRequestResult>(result);
            userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task UpdateUserReturnsOkResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<UserController>>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var userManagerMock = new Mock<TestUserManager>();

            var controller = new UserController(loggerMock.Object, userRepositoryMock.Object, userManagerMock.Object);

            int userIdToUpdate = 1; // Set the user ID to be updated
            var updatedUser = new User { Id = userIdToUpdate.ToString(), /* set other properties */ };

            userRepositoryMock.Setup(repo => repo.GetByIdAsync(userIdToUpdate))
                      .ReturnsAsync(new User { Id = userIdToUpdate.ToString() });
            // Act
            var result = await controller.UpdateUser(userIdToUpdate, updatedUser);

            // Assert
            // Verify that GetByIdAsync is called with the correct ID
            userRepositoryMock.Verify(repo => repo.GetByIdAsync(userIdToUpdate), Times.Once);

            // Verify that UpdateAsync is called with the correct user
            userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<User>()), Times.Once);

            // Assert the result
            Assert.IsType<OkResult>(result);
        
    }

        [Fact]
        public async Task DeleteUserReturnsOkResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<UserController>>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var userManagerMock = new Mock<TestUserManager>();

            var controller = new UserController(loggerMock.Object, userRepositoryMock.Object, userManagerMock.Object);

            int userIdToDelete = 1; // Set the user ID to be deleted

            // Act
            var result = await controller.DeleteUser(userIdToDelete);

            // Assert
            Assert.IsType<OkResult>(result);
            userRepositoryMock.Verify(repo => repo.DeleteAsync(userIdToDelete), Times.Once);
        }


        [Fact]
        public async Task GetAllUserArticlesReturnsOkResultWithUsers()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<UserController>>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var userManagerMock = new Mock<TestUserManager>();

            var expectedUsers = new List<User>
        {
            new User { Id = Guid.NewGuid().ToString() },
            new User { Id = Guid.NewGuid().ToString() },
            // Add more users as needed
        };

            userRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedUsers);

            var controller = new UserController(loggerMock.Object, userRepositoryMock.Object, userManagerMock.Object);

            // Act
            var result = await controller.GetAllUserArticles();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualUsers = Assert.IsAssignableFrom<List<User>>(okResult.Value);
            Assert.Equal(expectedUsers.Count, actualUsers.Count);
        }
    }
}
    
    
    












        
