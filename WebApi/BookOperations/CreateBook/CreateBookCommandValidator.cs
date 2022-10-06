using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
       public CreateBookCommandValidator()
        {
            RuleFor(c => c.Model.GenreId).GreaterThan(0);
            RuleFor(c => c.Model.PageCount).GreaterThan(0);
            RuleFor(c => c.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(c => c.Model.Title).NotEmpty().MinimumLength(4);
        }

    }
}

