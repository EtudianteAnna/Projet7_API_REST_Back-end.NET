using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Repositories;
using Microsoft.AspNet.Identity;

namespace P7CreateRestApi.Domain;

    public class UserControllerTests
{
        [Fact]
        public async Task AddUserReturnsOkResult()
        {
        // Arrange
        var loggerMock = new Mock<ILogger<UserController>>();
            var userRepositoryMock = new Mock<IUserRepository>();
        var userManagerMock = new Mock<Microsoft.AspNet.Identity.IUser<string>>();

            var controller = new UserController(loggerMock.Object, userRepositoryMock.Object,userManagerMock.Object);

            var userToAdd = new User
            {
                Id = "SomeUserId", // Définir un ID d'utilisateur valide
                                   // Définir les autres propriétés selon les besoins du test
            };

            // Act
            var result = await controller.AddUser(userToAdd);

            // Assert
            Assert.IsType<OkResult>(result);
            userRepositoryMock.Verify(repo => repo.AddAsync(userToAdd), Times.Once);
        }

        // Autres tests pour les autres méthodes du contrôleur UserController
    }

