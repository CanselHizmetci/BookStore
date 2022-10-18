
using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{

    public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenTheAuthorIsNotAvailable_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var authorId = 1;
            var author = _context.Authors.FirstOrDefault(c => c.Id == authorId);
            if (author != null)
            {
                _context.Authors.Remove(author);
                _context.SaveChanges();
            }
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(_context, _mapper);
            command.AuthorId = authorId;
            //Act
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar mevcut değil");

        }

        [Fact]
        public void WhenTheAuthorIsNotAvailable_Author_ShouldNotBeReturnErrors()
        {
            //Arrange
            var authorId = 1;
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(_context, _mapper);
            command.AuthorId = authorId;
            //Act
            FluentActions
                .Invoking(() => command.Handle()).Invoke();
            var author = _context.Authors.Find(authorId);
            author.Should().NotBeNull();

        }
    }
}

