using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                new Book
                {
                    //Id=1,
                    Title = "Book1",
                    GenreId = 1,
                    PageCount = 254,
                    PublishDate = new DateTime(2009, 05, 14),
                    AuthorId = 1
                },
                new Book
                {
                    //Id=2,
                    Title = "Book2",
                    GenreId = 2,
                    PageCount = 120,
                    PublishDate = new DateTime(2012, 02, 04),
                    AuthorId = 2
                },
                new Book
                {
                    //Id=3,
                    Title = "Book3",
                    GenreId = 3,
                    PageCount = 386,
                    PublishDate = new DateTime(2004, 12, 06),
                    AuthorId = 3
                }
            );
        }
    }
}

