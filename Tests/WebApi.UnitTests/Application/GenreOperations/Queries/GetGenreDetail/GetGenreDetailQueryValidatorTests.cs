using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenGenreIdLessThanZero_Validator_ShouldBeReturnError()
        {
            //Arrange
            GetGenreDetailQuery command = new GetGenreDetailQuery(null,null);
            command.GenreId = 0;

            //Act
            GenreDetailQueryValidator validator = new GenreDetailQueryValidator();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            GetGenreDetailQuery command = new GetGenreDetailQuery(null,null);
            command.GenreId = 1;
            GenreDetailQueryValidator validator = new GenreDetailQueryValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}

