using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0, "kitap türü")]
        [InlineData(1, "")]
        [InlineData(1, "fgh")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int id, string name)
        {
            //Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = id;
            command.Model = new UpdateGenreModel
            {
                Name=name
            };
            //Act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = 1;
            command.Model = new UpdateGenreModel
            {
                Name = "Kitap Türü"
            };
            //Act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}

