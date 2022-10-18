using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenAuthorIdLessThanZero_Validator_ShouldBeReturnError()
        {
            //Arrange
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(null,null);
            command.AuthorId = 0;

            //Act
            GetAuthorDetailValidator validator = new GetAuthorDetailValidator();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(null,null);
            command.AuthorId = 1;
            GetAuthorDetailValidator validator = new GetAuthorDetailValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}

