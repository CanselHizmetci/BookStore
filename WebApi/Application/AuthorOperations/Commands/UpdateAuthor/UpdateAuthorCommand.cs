using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }
        public UpdateAuthorModel Model { get; set; }
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.FirstOrDefault(c => c.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Güncellemek istediğiniz kitap mevcut değil");

            author.Name= String.IsNullOrEmpty(Model.Name.Trim()) ? author.Name : Model.Name;
            author.Surname = String.IsNullOrEmpty(Model.Surname.Trim()) ? author.Name : Model.Surname;

            _context.SaveChanges();
        }
    }
    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}

