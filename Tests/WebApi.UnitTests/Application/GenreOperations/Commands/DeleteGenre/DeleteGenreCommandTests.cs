
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTest:IClassFixture<CommonTestFixture>
    {
        
        private readonly IBookStoreDbContext _context;
        public DeleteGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        
        [Fact]
        public void WhenTheGenreIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var genreId = 1;
            var genre = _context.Genres.FirstOrDefault(c => c.Id == genreId);
            if(genre != null)
            { 
                _context.Genres.Remove(genre);
                _context.SaveChanges();
            }
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = genreId;
            //Act
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı");

        }
        [Fact]
        public void WhenValidInputsAreGivenNowIsGiven_DeleteGenre_ShouldNotBeReturnError()
        {
            var genreId = 1;
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = genreId;
            //Act
            FluentActions
                .Invoking(() => command.Handle()).Invoke();
        }
    }
}

