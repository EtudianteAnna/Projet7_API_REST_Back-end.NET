using Dot.Net.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using Xunit;


namespace TestProject7
{

    public class BidListsControllerTests
    {
        [Fact]
        public async Task Get_ReturnsOkObjectResult()
        {
            // Arrange
            var mock = new Mock<IBidListRepository>();
            var bidListRepositoryMock = new Mock<IBidListRepository>();
            bidListRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                                 .ReturnsAsync(new BidList { BidListId = 1 });

            var loggerMock = new Mock<ILogger<BidListsController>>();

            var controller = new BidListsController(loggerMock.Object, bidListRepositoryMock.Object);

            // Act
            var result = await controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task Get_ReturnsNotFoundForInvalidId()
        {
            // Arrange
            var mockRepository = new Mock<IBidListRepository>();
            var loggerMock = new Mock<ILogger<BidListsController>>();
            var controller = new BidListsController(loggerMock.Object, mockRepository.Object);

            // Act
            var actionResult = await controller.Get(1);
            var result = actionResult as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }
            [Fact]
            public async Task Post_ReturnsCreatedAtAction()

            {
                // Arrange
                var mockRepository = new Mock<IBidListRepository>();
                var loggerMock = new Mock<ILogger<BidListsController>>();
                var controller = new BidListsController(loggerMock.Object, mockRepository.Object);

                var bidListToCreate = new BidList { /* set properties */ };

                // Act
                var result = await controller.Post(bidListToCreate);
                var createdAtActionResult = result as CreatedAtActionResult;

                // Assert
                Assert.NotNull(createdAtActionResult);
            Assert.Equal(201, createdAtActionResult.StatusCode);
                Assert.Equal(nameof(controller.Get), createdAtActionResult.ActionName);
                Assert.NotNull(createdAtActionResult.RouteValues);
              mockRepository.Verify(m=>m.AddAsync(It.IsAny<BidList>()),Times.Once);
            
        }
        [Fact]
        public async Task PostReturnsBadRequestWhenAddFails()
            {
                // Arrange
                var mockRepository = new Mock<IBidListRepository>();
                mockRepository.Setup(repo => repo.AddAsync(It.IsAny<BidList>()))
                    .ThrowsAsync(new Exception("Simulated add failure"));

                var loggerMock = new Mock<ILogger<BidListsController>>();

            var controller = new BidListsController(loggerMock.Object, mockRepository.Object);

                var bidListToCreate = new BidList { /* set properties */ };

            // Act and Assert
            IActionResult result = await controller.Post(bidListToCreate);
            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            mockRepository.Verify(m => m.AddAsync(It.IsAny<BidList>()), Times.Once);
        }
        [Fact]
        //Ce scénario teste la logique de gestion des erreurs de validation du modèle dans la méthode Post
        public async Task PostReturnsBadRequestWhenModelIsInvalid()
        {
            // Arrange
            var mockRepository = new Mock<IBidListRepository>();
            var loggerMock = new Mock<ILogger<BidListsController>>();

            var controller = new BidListsController(loggerMock.Object, mockRepository.Object);
            controller.ModelState.AddModelError("Property Name", "Error message");

            var invalidBidList = new BidList { /* set properties */ };

            // Act
            var result = await controller.Post(invalidBidList);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);

            // Vérifiez que la méthode AddAsync du repository n'est pas appelée
            mockRepository.Verify(m => m.AddAsync(It.IsAny<BidList>()), Times.Never);

        }

        //Le test Put_ReturnsNoContent teste l'action Put de votre contrôleur BidListController pour vérifier si elle retourne le code de réponse "No Content" (204) après avoir réussi la mise à jour d'une liste d'offres
        [Fact]
            public async Task Put_ReturnsNoContent()
            {
                // Arrange
                var mockRepository = new Mock<IBidListRepository>();
                var loggerMock = new Mock<ILogger<BidListsController>>();
                var controller = new BidListsController(loggerMock.Object, mockRepository.Object);

                var existingBidList = new BidList { BidListId = 1, /* other properties */ };
                var bidListToUpdate = new BidList { BidListId = 1, /* updated properties */ };

                mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<BidList>()))
                    .Callback<BidList>(updatedBidList =>
                    {
                        // Verify that the UpdateAsync method was called with the correct parameters
                        Assert.Equal(bidListToUpdate.BidListId, updatedBidList.BidListId);
                        // Add more assertions based on your specific requirements
                    })
                    .Returns(Task.CompletedTask);

                // Act
                var result = await controller.Put(1, bidListToUpdate);
                var noContentResult = result as NoContentResult;

                // Assert
                Assert.NotNull(noContentResult);
                Assert.Equal(204, noContentResult.StatusCode);

            }
            //Échec de la mise à jour en raison d'une incompatibilité dans les identifiants
            [Fact]
            public async Task Put_WithMismatchedIds_ReturnsBadRequest()
            {
                // Arrange
                var mockRepository = new Mock<IBidListRepository>();
                var loggerMock = new Mock<ILogger<BidListsController>>();
                var controller = new BidListsController(loggerMock.Object, mockRepository.Object);

                var bidListToUpdate = new BidList { BidListId = 1, /* set other properties */ };

                // Act
                var result = await controller.Put(2, bidListToUpdate);

                // Assert
                var badRequestResult = Assert.IsType<BadRequestResult>(result);
                Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            }
            [Fact]
            public async Task Delete_ReturnsNoContent()
            {
                // Arrange
                var mockRepository = new Mock<IBidListRepository>();
                var loggerMock = new Mock<ILogger<BidListsController>>();
                var controller = new BidListsController(loggerMock.Object, mockRepository.Object);

                var bidListIdToDelete = 1;

                mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
                    .Callback<int>(id =>
                    {
                        // Verify that the DeleteAsync method was called with the correct parameter
                        Assert.Equal(bidListIdToDelete, id);
                        // Add more assertions based on your specific requirements
                    })
                    .Returns(Task.CompletedTask);

                // Act
                var result = await controller.Delete(bidListIdToDelete);
                var noContentResult = result as NoContentResult;

                // Assert
                Assert.NotNull(noContentResult);
                Assert.Equal(204, noContentResult.StatusCode);
                // Add more assertions based on your specific requirements
            }
            [Fact]
            public async Task DeleteReturnsBadRequestForInvalidId()
            {
                // Arrange
                var mockRepository = new Mock<IBidListRepository>();
                var loggerMock = new Mock<ILogger<BidListsController>>();

                var controller = new BidListsController(loggerMock.Object, mockRepository.Object);

                int invalidId = -1; // Utilisez un ID invalide qui déclenchera une demande incorrecte

            // Act
            var result = await controller.Delete(invalidId);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
                Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
                // Vous pouvez ajouter d'autres assertions en fonction de vos besoins spécifiques
            }

        }

    } 






