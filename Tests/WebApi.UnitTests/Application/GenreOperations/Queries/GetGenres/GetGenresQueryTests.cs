using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenresQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenQueryGetResult_Book_ShouldNotBeReturnErrors()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);

            FluentActions.Invoking(() => query.Handle()).Invoke();
        }
    }
}

