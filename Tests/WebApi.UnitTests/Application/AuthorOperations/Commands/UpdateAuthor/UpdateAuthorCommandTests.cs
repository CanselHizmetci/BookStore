
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenTheAuthorIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var authorId = 2;
            var author = _context.Authors.FirstOrDefault(c => c.Id == authorId);
            if (author != null)
            {
                _context.Authors.Remove(author);
                _context.SaveChanges();
            }
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = authorId;
            //Act
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellemek istediğiniz yazar mevcut değil");

        }
        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
        {
            //Arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            UpdateAuthorModel model = new UpdateAuthorModel()
            {
                Name = "test",
                Surname = "test",
                BirthDate = DateTime.Now.Date.AddYears(-10)
            };

            command.Model = model;
            command.AuthorId = 3;

            //Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert
            var author = _context.Authors.SingleOrDefault(c => c.Id == command.AuthorId);
            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.Surname.Should().Be(model.Surname);
            author.BirthDate.Should().Be(model.BirthDate);
        }
    }
}

