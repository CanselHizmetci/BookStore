using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBooks
{
    public class GetBooksQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBooksQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenQueryGetResult_Book_ShouldNotBeReturnErrors()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);

            FluentActions.Invoking(() => query.Handle()).Invoke();
        }
    }
}

