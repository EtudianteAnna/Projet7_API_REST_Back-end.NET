using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace P7CreateRestApi.Domain.Tests
{
    public class RuleNameTests
    {
        [Fact]
        public void RuleName_ShouldHaveRequiredProperties()
        {
            // Arrange
            var ruleName = new RuleName();

            // Act
            var validationResults = ValidateModel(ruleName);

            // Assert
            validationResults.Should().BeEmpty("because all properties are required");
        }

        [Fact]
        public void RuleName_ShouldValidateRequiredProperties()
        {
            // Arrange
            var ruleName = new RuleName
            {
                // Set some properties to null to trigger validation errors
                Name = null,
                Description = null,
                Json = null,
                Template = null,
                SqlStr = null,
                SqlPart = null
            };

            // Act
            var validationResults = ValidateModel(ruleName);

            // Assert
            validationResults.Should().HaveCount(6, "because all properties are required");
        }

        private static System.Collections.Generic.List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            Validator.TryValidateObject(model, context, validationResults, true);
            return validationResults;
        }
    }
}

