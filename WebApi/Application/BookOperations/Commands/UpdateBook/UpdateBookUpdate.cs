using System;
using System.Linq;
using WebApi.DBOperations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookUpdate
    {
        public UpdateBookModel Model { get; set; }
        public int BookId { get; set; }
        private readonly IBookStoreDbContext _dbContext;

        public UpdateBookUpdate(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var book = _dbContext.Books.FirstOrDefault(c => c.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Güncellemek istediğiniz kitap mevcut değil");
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;

            _dbContext.SaveChanges();
        }
       
    }
    public class UpdateBookModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
            public int GenreId { get; set; }
        }
}

