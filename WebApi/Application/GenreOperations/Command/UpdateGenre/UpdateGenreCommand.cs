using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _context;
       
        public UpdateGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(c => c.Id == GenreId);
            if (genre is null)
                throw new InvalidOperationException("Güncellemek istediğiniz kitap mevcut değil");

            if (_context.Genres.Any(c => c.Name.ToLower() == Model.Name.ToLower() && c.Id != GenreId))
                throw new InvalidOperationException("Aynı isimli kitap türü zaten mevcut");

            genre.Name = String.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
            genre.IsActive = Model.IsActive;
            _context.SaveChanges();
        }

        public class UpdateGenreModel
        {
            public string Name { get; set; }
            public bool IsActive { get; set; } = true;
        }
    }
}

