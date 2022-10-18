using System;
using FluentValidation;

namespace WebApi.Application.BookOperations.Queries.GetById
{
    public class GetByIdQueryValidator: AbstractValidator<GetByIdQuery>
    {
        public GetByIdQueryValidator()
        {
            RuleFor(c => c.BookId).GreaterThan(0);
        }
    }
}

