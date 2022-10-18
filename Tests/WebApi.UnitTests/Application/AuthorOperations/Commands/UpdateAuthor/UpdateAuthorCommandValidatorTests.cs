using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        public static readonly object[][] CorrectData =
        {
            new object[] { 1, "cans", "Hizmet", new DateTime(2000, 4, 20)},
            new object[] { 1, "cansel", "", new DateTime(2000, 4, 20) },
            new object[] { 1, "cansel", "hizmet", null },
            new object[] { 0, "cansel", "hizmet", new DateTime(2000, 4, 20) },
        };
        [Theory]
        [MemberData(nameof(CorrectData))]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int id, string name, string surname, DateTime birthDate)
        {
            //Arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = id;
            command.Model = new UpdateAuthorModel
            {
                Name=name,
                Surname=surname,
                BirthDate=birthDate
            };
            //Act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorModel
            {
                Name = "Cansel",
                Surname = "Hizmetçi",
                BirthDate = new DateTime(1999, 4, 20)
            };
            //Act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}

