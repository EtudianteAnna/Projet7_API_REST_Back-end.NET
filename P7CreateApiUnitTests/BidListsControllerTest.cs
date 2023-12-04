using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using Xunit;
using Assert = Xunit.Assert;

public class BidListsControllerTest
{
    public object BidListsController { get; private set; }

    [Fact]
    public async Task GetBidLists_ReturnOkResult_WithValidData()
    {
        //Arrange
        var mockRepository = new Mock<IBidListRepository>();
        BidListsController bidListsController = new BidListsController(mockRepository.Object);
        var controller = bidListsController;

        var fakeBidLists = new List<BidList>
        {
         new BidList {BidListId=1, /* autres propriétés*/} ,
         new BidList{BidListId=2 ,/*autres propriétés*/},
            //Ajoutez autant d'éléments que nécessaire

        };
        mockRepository.Setup(repo => repo.GetBidListsAsync()).ReturnsAsync(fakeBidLists);

        //Act
       var result = await controller.GetBidLists();

        //Assert
        var okresult = Assert.IsType<OkObjectResult>(result.Result);
        var actualValue = okresult.Value as IEnumerable<BidList>;
        Assert.NotNull(actualValue);
        Assert.Equal(fakeBidLists.Count,actualValue.Count());
    }

    [Fact]

    public async Task GetByIdAsync_ReturnOkResult_WithValidId()
    {

        //Arrange

    var mockRepository= new Mock<IBidListRepository>(); 
    BidListsController bidListsController= new BidListsController(mockRepository.Object);
    var controller = bidListsController;
        int validId = 1;
        var fakeBidList=new BidList { BidListId=validId, } ;
        mockRepository.Setup(repo=> repo.GetByIdAsync(validId)).ReturnsAsync(fakeBidList);

        //Act
        var result = await controller.GetBidList(validId);

        //Assert
        var okResult= Assert.IsType<OkObjectResult>(result.Result); 
        var bidList=Assert.IsAssignableFrom<BidList>(okResult.Value);
     Assert.Equal(fakeBidList.BidListId,bidList.BidListId);

      
    }


    [Fact]
    public async Task GetBidList_ReturnNotFound_WithInvalidId()
    {
        // Arrange
        var mockRepository = new Mock<IBidListRepository>();
        BidListsController bidListsController = new BidListsController(mockRepository.Object);
        var controller = bidListsController;

        int invalidId = 999; // Utilisez un ID qui n'existe pas dans votre jeu de données simulé

        mockRepository.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((BidList)null);

        // Act
        var result = await controller.GetBidList(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
    [Fact]
    public async Task PutBidList_ReturnsNoContent_WithValidIdAndMatchingBidListId()
    {
        // Arrange
        var mockRepository = new Mock<IBidListRepository>();
        BidListsController bidListsController = new BidListsController(mockRepository.Object);
        var controller = bidListsController;

        int validId = 1;
        var fakeBidList = new BidList { BidListId = validId /* autres propriétés */ };

        mockRepository.Setup(repo => repo.UpdateAsync(fakeBidList)).Returns(Task.CompletedTask);

        // Act
        var result = await controller.PutBidList(validId, fakeBidList);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task PostBidList_ReturnsCreatedAtAction_WithValidBidList()
    {
        // Arrange
        var mockRepository = new Mock<IBidListRepository>();
        BidListsController bidListsController = new BidListsController(mockRepository.Object);
        var controller = bidListsController;

        var fakeBidList = new BidList { /* autres propriétés */ };

        mockRepository.Setup(repo => repo.AddAsync(fakeBidList)).Returns(Task.CompletedTask);

        // Act
        var result = await controller.PostBidList(fakeBidList);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(BidListsController.ToString), createdAtActionResult.ActionName);
    }

    [Fact]
    public async Task DeleteBidList_ReturnsNoContent_WithValidIdAndExistingBidList()
    {
        // Arrange
        var mockRepository = new Mock<IBidListRepository>();
        BidListsController bidListsController = new BidListsController(mockRepository.Object);
        var controller = bidListsController;

        int validId = 1;
        var fakeBidList = new BidList { BidListId = validId /* autres propriétés */ };

        mockRepository.Setup(repo => repo.GetByIdAsync(validId)).ReturnsAsync(fakeBidList);
        mockRepository.Setup(repo => repo.DeleteAsync(validId)).Returns(Task.CompletedTask);

        // Act
        var result = await controller.DeleteBidList(validId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteBidList_ReturnsNotFound_WithNonExistingBidList()
    {
        // Arrange
        var mockRepository = new Mock<IBidListRepository>();
        BidListsController bidListsController = new BidListsController(mockRepository.Object);
        var controller = bidListsController;

        int nonExistingId = 999; // Utilisez un ID qui n'existe pas dans votre jeu de données simulé

        mockRepository.Setup(repo => repo.GetByIdAsync(nonExistingId)).ReturnsAsync((BidList)null);

        // Act
        var result = await controller.DeleteBidList(nonExistingId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}

