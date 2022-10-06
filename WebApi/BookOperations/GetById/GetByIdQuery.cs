using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.BookOperations.GetById
{
    public class GetByIdQuery
    {
        public BookViewModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public GetByIdQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Book Handle(int id)
        {
            var book = _dbContext.Books.Where(c => c.Id == id).FirstOrDefault();
            Model = new BookViewModel();
            Model.Title = book.Title;
            Model.PageCount = book.PageCount;
            Model.Genre = ((GenreEnum)book.GenreId).ToString();
            Model.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy");
            return book;
        }

        public class BookViewModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string Genre { get; set; }
        }
    }
}

