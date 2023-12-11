using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace TestProject7
{ 

    public class RuleNamesControllerTests
    {
        [Fact]
        public async Task GetRuleNamesReturnsRuleNames()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<RuleNamesController>>();
            var mockContext = new Mock<LocalDbContext>();
            var controller = new RuleNamesController(mockContext.Object);

            var ruleNames = new List<RuleName> { /* initialize rule names */ };
            mockContext.Setup(c => c.RuleNames).ReturnsDbSet(ruleNames);

            // Act
            var result = await controller.GetRuleNames();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<RuleName>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var model = Assert.IsType<List<RuleName>>(okResult.Value);
            Assert.Equal(ruleNames.Count, model.Count);
        }

        [Fact]
        public async Task GetRuleNameReturnsRuleName()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<RuleNamesController>>();
            var mockContext = new Mock<LocalDbContext>();
            var controller = new RuleNamesController(mockContext.Object);

            var ruleName = new RuleName { /* initialize rule name */ };
            mockContext.Setup(c => c.RuleNames.Find(It.IsAny<int>())).Returns(ruleName);

            // Act
            var result = await controller.GetRuleName(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<RuleName>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var model = Assert.IsType<RuleName>(okResult.Value);
            Assert.Equal(ruleName.Id, model.Id);
        }

        [Fact]
        public async Task PostRuleNameReturnsCreatedAtActionWhenAddSuccessful()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<RuleNamesController>>();
            var mockContext = new Mock<LocalDbContext>();
            var controller = new RuleNamesController(mockContext.Object);

            var ruleName = new RuleName { /* initialize rule name */ };

            mockContext.Setup(c => c.RuleNames.Add(It.IsAny<RuleName>()));
            mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1); // Utilisez It.IsAny<CancellationToken>() si nécessaire

            // Act
            var result = await controller.PostRuleName(ruleName);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var model = Assert.IsType<RuleName>(createdAtActionResult.Value);
            mockContext.Verify(c => c.RuleNames.Add(It.IsAny<RuleName>()), Times.Once);
            mockContext.Verify(c => c.SaveChangesAsync(default(CancellationToken)), Times.Once);
        }



        [Fact]
        public async Task DeleteRuleNameReturnsNoContentWhenDeleteSuccessful()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<RuleNamesController>>();
            var mockContext = new Mock<LocalDbContext>();
            var controller = new RuleNamesController(mockContext.Object);

            var ruleName = new RuleName { /* initialize rule name */ };

            // Configure the DbSet property on the context to return a DbSet with the ruleName
            var mockDbSet = new Mock<DbSet<RuleName>>();
            mockDbSet.Setup(d => d.FindAsync(It.IsAny<int>())).ReturnsAsync(ruleName);
            mockContext.Setup(c => c.RuleNames).Returns(mockDbSet.Object);

            mockContext.Setup(c => c.RuleNames.Remove(It.IsAny<RuleName>()));

            mockContext.Setup(c => c.SaveChangesAsync(default(CancellationToken))).ReturnsAsync(1); // Utilisez It.IsAny<CancellationToken>() si nécessaire

            // Act
            var result = await controller.DeleteRuleName(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
            mockContext.Verify(c => c.RuleNames.Remove(It.IsAny<RuleName>()), Times.Once);
            mockContext.Verify(c => c.SaveChangesAsync(default(CancellationToken)), Times.Once);
        }




        // Add more test methods as needed

        private class DbSetMock<TEntity> : Mock<DbSet<TEntity>> where TEntity : class
        {
            public static DbSet<TEntity> Create(List<TEntity> data)
            {
                var queryable = data.AsQueryable();
                var dbSetMock = new DbSetMock<TEntity>();
                dbSetMock.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(queryable.Provider);
                dbSetMock.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(queryable.Expression);
                dbSetMock.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
                dbSetMock.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
                return dbSetMock.Object;
            }
        }
    }
}
