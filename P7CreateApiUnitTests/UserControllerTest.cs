using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using Xunit;
using Assert = Xunit.Assert;

namespace P7CreateRestApi.Tests.Controllers
{
    public class UserControllerTests
    {
        [Fact]
        public async Task GetUserLists_ReturnsListOfBidLists_WhenExists()
        {
            // Arrange
            var mockRepository = new Mock<IBidListRepository>();
            var controller = new UserController(mockRepository.Object);
            var fakeBidLists = new List<BidList> { new BidList { BidListId = 1, /* other properties */ } };
            mockRepository.Setup(repo => repo.GetBidListsAsync()).ReturnsAsync(fakeBidLists);

            // Act
            var result = await controller.GetUserLists();

            // Assert
            var okObjectResult = Xunit.Assert.IsType<OkObjectResult>(result);
            okObjectResult.Value.Should().BeAssignableTo<IEnumerable<BidList>>();
            var bidLists = (IEnumerable<BidList>)okObjectResult.Value;
            bidLists.Should().NotBeNull();
            bidLists.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task GetUserList_ReturnsBidList_WithValidId()
        {
            // Arrange
            var mockRepository = new Mock<IBidListRepository>();
            var controller = new UserController(mockRepository.Object);
            var bidListId = 1;
            var fakeBidList = new BidList { BidListId = bidListId, /* other properties */ };
            mockRepository.Setup(repo => repo.GetByIdAsync(bidListId)).ReturnsAsync(fakeBidList);

            // Act
            var result = await controller.GetUserList(bidListId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            okObjectResult.Value.Should().BeAssignableTo<BidList>();
            var bidList = (BidList)okObjectResult.Value;
            bidList.Should().NotBeNull();
            bidList.BidListId.Should().Be(bidListId);
        }
        [Fact]
        public async Task PostUserList_ReturnsCreatedAtAction_WithValidData()
        {
            // Arrange
            var mockRepository = new Mock<IBidListRepository>();
            var controller = new UserController(mockRepository.Object);
            var bidListToAdd = new BidList { BidListId = 1, /* other properties */ };

            // Act
            var result = await controller.PostUserList(bidListToAdd);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            createdAtActionResult.ActionName.Should().Be(nameof(UserController.GetUserList));

            var bidList = Assert.IsType<BidList>(createdAtActionResult.Value);
            bidList.BidListId.Should().Be(bidListToAdd.BidListId);
        }

        [Fact]
        public async Task PutUserList_ReturnsNoContentResult_WithValidData()
        {
            // Arrange
            var mockRepository = new Mock<IBidListRepository>();
            var controller = new UserController(mockRepository.Object);
            var bidListToUpdate = new BidList { BidListId = 1, /* other properties */ };

            // Act
            var result = await controller.PutUserList(bidListToUpdate.BidListId, bidListToUpdate);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteUserList_ReturnsNoContentResult_WithValidId()
        {
            // Arrange
            var mockRepository = new Mock<IBidListRepository>();
            var controller = new UserController(mockRepository.Object);
            var bidListIdToDelete = 1;

            // Act
            var result = await controller.DeleteUserList(bidListIdToDelete);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

    }
}
  