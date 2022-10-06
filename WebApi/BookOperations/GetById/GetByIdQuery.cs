using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        public int BookId { get; set; }
        private readonly IMapper _mapper;

        public GetByIdQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookViewModel Handle(int id)
        {
            var book = _dbContext.Books.Where(c => c.Id == BookId).FirstOrDefault();
            if (book is null)
                throw new InvalidOperationException("Kitap Bulunamadı");

            Model = _mapper.Map<BookViewModel>(book);//new BookViewModel();

            /*Model.Title = book.Title;
            Model.PageCount = book.PageCount;
            Model.Genre = ((GenreEnum)book.GenreId).ToString();
            Model.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy");*/
            return Model;
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

