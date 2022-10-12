using System;
using System.Linq;
using AutoMapper;
using FluentValidation;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailValidator : AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailValidator()
        {
            RuleFor(c => c.AuthorId).GreaterThan(0);
        }
    }
}

