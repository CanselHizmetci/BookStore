
using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    
    public class CreateAuthorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistAuthorTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange (Hazırlık)
            var author = new Author()
            {
                Name = "Test_WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn",
                Surname = "Test_WhenAlreadyExistAuthorSurnameIsGiven_InvalidOperationException_ShouldBeReturn"
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorModel() { Name = author.Name, Surname=author.Surname };

            //Act & Assert (Çalıştırma-Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut");
        
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            //Arrange
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            CreateAuthorModel model = new CreateAuthorModel()
            {
                Name = "WhenValidInputsAreGiven_Author_ShouldBeCreated",
                Surname = "WhenValidInputsAreGiven_Author_ShouldBeCreated",
                BirthDate = DateTime.Now.Date.AddYears(-10)
            };

            command.Model = model;

            //Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert
            var author = _context.Authors.SingleOrDefault(c => c.Name == model.Name && c.Surname==model.Surname);
            author.Should().NotBeNull();
            author.BirthDate.Should().Be(model.BirthDate);
        }
    }
    
}

