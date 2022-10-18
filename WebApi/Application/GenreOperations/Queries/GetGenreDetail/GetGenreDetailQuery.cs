using System;
using AutoMapper;
using WebApi.DBOperations;
using System.Linq;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{ 
 public class GetGenreDetailQuery
{
    public int GenreId { get; set; }
    public readonly IBookStoreDbContext _context;
    public readonly IMapper _mapper;
    public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public GenreDetailViewModel Handle()
    {
        var genre = _context.Genres.Where(c => c.IsActive && c.Id ==GenreId).FirstOrDefault();
            if (genre is null)
                throw new InvalidOperationException("Kitap türü bulunamadı");


        return _mapper.Map<GenreDetailViewModel>(genre);
        
    }
}

    public class GenreDetailViewModel
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
}
