using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public AuthorViewModel Model { get; set; }
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public AuthorViewModel Handle()
        {
            var author = _context.Authors.Where(c => c.Id == AuthorId).FirstOrDefault();
            if (author is null)
                throw new InvalidOperationException("Yazar mevcut değil");

            return _mapper.Map<AuthorViewModel>(author);
        }
    }

    public class AuthorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
    }
}

