using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        public static readonly object[][] CorrectData =
        {
            new object[] { "cans", "Hizmet", new DateTime(2000, 4, 20)},
            new object[] { "cansel", "", new DateTime(2000, 4, 20) },
            new object[] { "cansel", "hizmet", null },
        };
        [Theory]
        [MemberData(nameof(CorrectData))]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string name, string surname, DateTime birthdate)
        {
            //Arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel
            {
                 Name=name,
                 Surname=surname,
                 BirthDate=birthdate
            };
            //Act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_Validator_ShouldNotBeReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel
            {
                Name = "cansel",
                Surname = "Hizmet",
                BirthDate = new DateTime(2000, 4, 20)
            };
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}

