using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTest:IClassFixture<CommonTestFixture>
    {
        
        private readonly IBookStoreDbContext _context;
        public DeleteBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        
        [Fact]
        public void WhenTheBookIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var bookId = 1;
            var book = _context.Books.FirstOrDefault(c => c.Id == bookId);
            if(book!=null)
            { 
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = bookId;
            //Act
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek Kitap Bulunamadı");

        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_DeleteBook_ShouldNotBeReturnError()
        {
            var bookId = 2;
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = bookId;
            //Act
            FluentActions
                .Invoking(() => command.Handle()).Invoke();
        }
    }
}

