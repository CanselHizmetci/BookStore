using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _context;

        public DeleteAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.Include(c=> c.Books).FirstOrDefault(c => c.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Silmek istediğiniz yazar mevcut değil");

            if(author.Books.Any())
                throw new InvalidOperationException("Silmek istediğiniz yazarın yayında kitabı mevcut");

            _context.Authors.Remove(author);
            _context.SaveChanges();
                   
        }
    }
}

