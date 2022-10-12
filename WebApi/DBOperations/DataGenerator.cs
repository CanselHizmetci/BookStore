using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.DBOperations
{
    [Route("api/[controller]")]
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                // Look for any book.
                if (context.Books.Any())
                {
                    return;   // Data was already seeded
                }
                context.Genres.AddRange(

                    new Genre
                    {
                        Name = "Personal Growth"
                    },

                    new Genre
                    {
                        Name = "Science Fiction"
                    },

                    new Genre
                    {
                        Name = "Romance"
                    }
                    );
                context.Authors.AddRange(

                    new Author
                    {
                        Name = "Cansel",
                        Surname="Hizmetçi",
                        BirthDate= new DateTime(1999,04,20)
                    },
                    new Author
                    {
                        Name = "Sercan",
                        Surname = "Adan",
                        BirthDate = new DateTime(1987,04,04)
                    },
                    new Author
                    {
                        Name = "Ali",
                        Surname = "Hizmetçi",
                        BirthDate = new DateTime(2000,11,13)
                    }
                    ) ;
                context.Books.AddRange(
                new Book
                {
                   //Id=1,
                   Title= "Book1",
                   GenreId=1,
                   PageCount=254,
                   PublishDate= new DateTime(2009,05,14),
                   AuthorId=1
                },
                new Book
                {
                   //Id=2,
                   Title= "Book2",
                   GenreId=2,
                   PageCount=120,
                   PublishDate= new DateTime(2012,02,04),
                   AuthorId = 2
                },
                new Book
                {
                   //Id=3,
                   Title= "Book3",
                   GenreId=3,
                   PageCount=386,
                   PublishDate= new DateTime(2004,12,06),
                   AuthorId = 3
                }
            );

                context.SaveChanges();
            }
        }
    }
}

