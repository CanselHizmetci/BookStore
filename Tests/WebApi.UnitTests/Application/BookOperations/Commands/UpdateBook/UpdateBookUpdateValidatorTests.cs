using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookUpdateValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,"Book4", 1, 1)]
        [InlineData(1,"", 1, 1)]
        [InlineData(1,"Book4", 0, 1)]
        [InlineData(1, "Book4", 10, 0)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int id, string title, int pageCount, int genreId)
        {
            //Arrange
            UpdateBookUpdate command = new UpdateBookUpdate(null);
            command.BookId = id;
            command.Model = new UpdateBookModel
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId
            };
            //Act
            UpdateBookUpdateValidator validator = new UpdateBookUpdateValidator();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            UpdateBookUpdate command = new UpdateBookUpdate(null);
            command.BookId = 1;
            command.Model = new UpdateBookModel
            {
                Title = "Book4",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };
            //Act
            UpdateBookUpdateValidator validator = new UpdateBookUpdateValidator();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
            
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            UpdateBookUpdate command = new UpdateBookUpdate(null);
            command.BookId = 1;
            command.Model = new UpdateBookModel
            {
                Title = "Book4",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1
            };
            //Act
            UpdateBookUpdateValidator validator = new UpdateBookUpdateValidator();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}

