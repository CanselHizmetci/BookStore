
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTest:IClassFixture<CommonTestFixture>
    {
        
        private readonly IBookStoreDbContext _context;
        public DeleteAuthorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenTheAuthorIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var authorId = 1;
            var author = _context.Authors.FirstOrDefault(c => c.Id == authorId);
            if (author != null)
            {
                _context.Books.RemoveRange(author.Books.ToList());
                _context.SaveChanges();
                _context.Authors.Remove(author);
                _context.SaveChanges();
            }
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = authorId;
            //Act
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silmek istediğiniz yazar mevcut değil");

        }
        [Fact]
        public void WhenTheAuthorHasBook_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var authorId = 3;
            var author = _context.Authors.Find(authorId);
            if (author.Books == null)
            {
                author.Books.Add(new Entities.Book
                {
                    GenreId = 1,
                    PageCount = 100,
                    PublishDate = DateTime.Now.AddYears(-3),
                    Title = "Temp Book"
                });

                _context.SaveChanges();
            }
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = authorId;
            //Act
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silmek istediğiniz yazarın yayında kitabı mevcut");

        }
        [Fact]
        public void WhenValidInputsAreGiven_DeleteAuthor_ShouldNotBeReturnError()
        {
            var authorId = 2;
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = authorId;
            if (_context.Authors.Find(authorId)?.Books != null)
            {
                _context.Books.RemoveRange(_context.Books.Where(c=>c.AuthorId==authorId).ToList());
                _context.SaveChanges();
            }
            //Act
            FluentActions
                .Invoking(() => command.Handle()).Invoke();
        }
    }
}

