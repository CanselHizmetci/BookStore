using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.GetBooks;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.GetById.GetByIdQuery;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Common
{
    [Route("api/[controller]")]
    public class MappingProfile : Profile
    {
       public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookViewModel>().ForMember(c => c.Genre, c => c.MapFrom(c => ((GenreEnum)c.GenreId).ToString()));
            CreateMap<Book, BooksViewModel>().ForMember(c => c.Genre, c => c.MapFrom(c => ((GenreEnum)c.GenreId).ToString()));
        }
    }
}

