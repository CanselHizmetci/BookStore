
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenTheGenreIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var genreId = 1;
            var genre = _context.Genres.FirstOrDefault(c => c.Id == genreId);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                _context.SaveChanges();
            }
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = genreId;
            //Act
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellemek istediğiniz kitap mevcut değil");

        }
        [Fact]
        public void WhenTheGenreNameAlreadyExist_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var genreId = 2;
            var genre = _context.Genres.FirstOrDefault(c => c.Id == 1);
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.Model = new UpdateGenreModel
            {
                Name = genre.Name
            };
            command.GenreId = genreId;
            //Act
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı isimli kitap türü zaten mevcut");

        }
        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
        {
            //Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            UpdateGenreModel model = new UpdateGenreModel()
            {
                Name = "Valid Genre Name"
            };

            command.Model = model;
            command.GenreId = 2;

            //Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert
            var genre = _context.Genres.SingleOrDefault(c => c.Id == command.GenreId);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
        }
    }
}

