using System;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookUpdateTests:IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        public UpdateBookUpdateTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
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
            UpdateBookUpdate command = new UpdateBookUpdate(_context);
            command.BookId = bookId;
            //Act
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellemek istediğiniz kitap mevcut değil");

        }
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            //Arrange
            UpdateBookUpdate command = new UpdateBookUpdate(_context);
            UpdateBookModel model = new UpdateBookModel() { Title = "Book5", PageCount = 1000, PublishDate = DateTime.Now.Date.AddYears(-10), GenreId = 1 };

            command.Model = model;
            command.BookId = 2;

            //Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert
            var book = _context.Books.SingleOrDefault(c => c.Id == command.BookId);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.GenreId.Should().Be(model.GenreId);
            book.PublishDate.Should().Be(model.PublishDate);
        }
    }
}

