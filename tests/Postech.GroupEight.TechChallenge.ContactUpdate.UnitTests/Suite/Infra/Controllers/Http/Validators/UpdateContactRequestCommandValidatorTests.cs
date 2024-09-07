using Bogus;
using FluentValidation.TestHelper;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Controllers.Http.Commands;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Controllers.Http.Validators;

namespace Postech.GroupEight.TechChallenge.ContactUpdate.UnitTests.Suite.Infra.Controllers.Http.Validators
{
    public class DeleteContactRequestCommandValidatorTests
    {
        private readonly DeleteContactRequestCommandValidator _validator;
        private readonly Faker _faker = new("pt_BR");

        public DeleteContactRequestCommandValidatorTests()
        {
            _validator = new();
        }

        [Fact(DisplayName = "All command properties are valid")]
        [Trait("Action", "DeleteContactRequestCommandValidator")]
        public void DeleteContactRequestCommandValidator_AllCommandPropertiesAreValid_ShouldNotHaveValidationErrors()
        {
            // Arrange
            DeleteContactRequestCommand command = new()
            {
                ContactId = Guid.NewGuid()
            };

            // Act
            TestValidationResult<DeleteContactRequestCommand> result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(c => c.ContactId);
        }

        [Fact(DisplayName = "Contact identification property is invalid")]
        [Trait("Action", "DeleteContactRequestCommandValidator")]
        public void DeleteContactRequestCommandValidator_ContactIdentificationPropertyIsInvalid_ShouldHaveValidationErrors()
        {
            // Arrange
            DeleteContactRequestCommand command = new()
            {
                ContactId = Guid.Empty
            };

            // Act
            TestValidationResult<DeleteContactRequestCommand> result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.ContactId);
        }
    }
}