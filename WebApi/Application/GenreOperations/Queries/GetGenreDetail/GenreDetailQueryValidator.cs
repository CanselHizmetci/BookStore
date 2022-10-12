using System;
using FluentValidation;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GenreDetailQueryValidator:AbstractValidator<GetGenreDetailQuery>
    { 
        public GenreDetailQueryValidator()
        {
            RuleFor(c => c.GenreId).GreaterThan(0);
        }
    }
}

