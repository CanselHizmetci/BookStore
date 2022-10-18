using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(

                    new Author
                    {
                        Name = "Cansel",
                        Surname = "Hizmetçi",
                        BirthDate = new DateTime(1999, 04, 20)
                    },
                    new Author
                    {
                        Name = "Sercan",
                        Surname = "Adan",
                        BirthDate = new DateTime(1987, 04, 04)
                    },
                    new Author
                    {
                        Name = "Ali",
                        Surname = "Hizmetçi",
                        BirthDate = new DateTime(2000, 11, 13)
                    }
                    );
        }
    }
}

