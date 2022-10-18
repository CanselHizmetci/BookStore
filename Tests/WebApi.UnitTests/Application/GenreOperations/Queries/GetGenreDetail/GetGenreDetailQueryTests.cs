
using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{

    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenTheGenreIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var genreId = 2;
            var genre = _context.Genres.FirstOrDefault(c => c.Id == genreId);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                _context.SaveChanges();
            }
            GetGenreDetailQuery command = new GetGenreDetailQuery(_context, _mapper);
            command.GenreId = genreId;
            //Act
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı");

        }

        [Fact]
        public void WhenTheGenreIsNotAvailable_Genre_ShouldNotBeReturnErrors()
        {
            //Arrange
            var genreId = 1;
            GetGenreDetailQuery command = new GetGenreDetailQuery(_context, _mapper);
            command.GenreId = genreId;
            //Act
            FluentActions
                .Invoking(() => command.Handle()).Invoke();
            var genre = _context.Genres.Find(genreId);
            genre.Should().NotBeNull();

        }
    }
}

