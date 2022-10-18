
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetById;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.QueriesGetById
{

    public class GetByIdQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetByIdQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenTheBookIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var bookId = 1;
            var book = _context.Books.FirstOrDefault(c => c.Id == bookId);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
            GetByIdQuery command = new GetByIdQuery(_context, _mapper);
            command.BookId = bookId;
            //Act
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadı");

        }

        [Fact]
        public void WhenTheBookIsNotAvailable_Book_ShouldNotBeReturnErrors()
        {
            //Arrange
            var bookId = 2;
            GetByIdQuery command = new GetByIdQuery(_context, _mapper);
            command.BookId = bookId;
            //Act
            FluentActions
                .Invoking(() => command.Handle()).Invoke();
            var book = _context.Books.Find(bookId);
            book.Should().NotBeNull();

        }
    }
}

