using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;
using P7CreateRestApi.Repositories;
using P7CreateRestApi.Domain;


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

            

        }

        [Fact]

        public async Task AddUserReturnsNoResult()
        {

            // Arrange
            var loggerMock = new Mock<ILogger<UserController>>();
            var userRepositoryMock = new Mock<IUserRepository>(); 
            var userManagerMock = new Mock<TestUserManager>();


            var controller = new UserController(loggerMock.Object, userRepositoryMock.Object, userManagerMock.Object);

            var userToAdd = new User { /* set properties */ };

            // Act
            var result = await controller.AddUser(userToAdd);

            // Assert
            Assert.IsType<OkResult>(result);

        }

        [Fact]
        public async Task ValidateReturnsOkResultWhenModelStateIsValide()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<UserController>>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var userManagerMock = new Mock<TestUserManager>();


            var controller = new UserController(loggerMock.Object, userRepositoryMock.Object, userManagerMock.Object);
            controller.ModelState.AddModelError("PropertyName", "Error message");

            var userToValidate = new User { /* set properties */ };

            // Act
            var result = await controller.Validate(userToValidate);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task ValidateReturnsBadRequestResultWhenModelStateIsInvalid()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<UserController>>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var userManagerMock = new Mock<TestUserManager>();


            var controller = new UserController(loggerMock.Object, userRepositoryMock.Object, userManagerMock.Object);
            controller.ModelState.AddModelError("PropertyName", "Error message");

            var userToValidate = new User { /* set properties */ };

            // Act
            var result = await controller.Validate(userToValidate);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteUserReturnsOkResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<UserController>>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var userManagerMock = new Mock<TestUserManager>();



            var controller = new UserController(loggerMock.Object, userRepositoryMock.Object, userManagerMock.Object);

            int userIdToDelete = 1;

            // Act
            var result = await controller.DeleteUser(userIdToDelete);

            // Assert
            Assert.IsType<OkResult>(result);
        }


        [Fact]
        public async Task GetAllUserArticlesReturnsOkResultWithUsers()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<UserController>>();
            var mockRepository = new Mock<IUserRepository>();
            var userManagerMock = new Mock<TestUserManager>();

            var users = new List<User> { new User { Id = Guid.NewGuid().ToString() } };
            mockRepository
                .Setup(repo => repo.GetAllAsync())
               .ReturnsAsync(users);
                                       
                

            var controller = new UserController(mockLogger.Object, mockRepository.Object, userManagerMock.Object) { };
                        
            // Act
            var result = await controller.GetAllUserArticles();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUsers = Assert.IsAssignableFrom<List<User>>(okResult.Value);
            Assert.Equal(users.Count, returnedUsers.Count);
        }
    

        [Fact]
        public async Task GetAllUserArticlesReturnsOkResultWithoutUsers()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var mockLogger = new Mock<ILogger<UserController>>();
            var userManagerMock = new Mock<TestUserManager>();

            mockRepository
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<User>()); // Simulate an empty list

            var controller = new UserController(mockLogger.Object, mockRepository.Object, userManagerMock.Object);

            // Act
            var result = await controller.GetAllUserArticles();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUsers = Assert.IsAssignableFrom<List<User>>(okResult.Value);
            Assert.Empty(returnedUsers);
        }

    }


}
