using System;
using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookUpdateValidator: AbstractValidator<UpdateBookUpdate>
    {
        public UpdateBookUpdateValidator()
        {
            RuleFor(c => c.BookId).GreaterThan(0);
            RuleFor(c => c.Model.PageCount).GreaterThan(0);
            RuleFor(c => c.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(c => c.Model.GenreId).GreaterThan(0);
            RuleFor(c => c.Model.Title).NotEmpty().MinimumLength(4);
        }
    }
}

