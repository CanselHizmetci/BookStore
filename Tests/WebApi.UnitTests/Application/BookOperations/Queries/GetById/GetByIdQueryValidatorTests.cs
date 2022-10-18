using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetById;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetById
{
    public class GetByIdQueryValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenBookIdLessThanZero_Validator_ShouldBeReturnError()
        {
            //Arrange
            GetByIdQuery command = new GetByIdQuery(null,null);
            command.BookId = 0;

            //Act
            GetByIdQueryValidator validator = new GetByIdQueryValidator();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            GetByIdQuery command = new GetByIdQuery(null,null);
            command.BookId = 1;
            GetByIdQueryValidator validator = new GetByIdQueryValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}

