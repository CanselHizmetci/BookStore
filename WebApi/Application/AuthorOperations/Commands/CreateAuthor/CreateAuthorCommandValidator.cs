using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
       public CreateAuthorCommandValidator()
        {
            RuleFor(c => c.Model.Name).NotEmpty().MinimumLength(5);
            RuleFor(c => c.Model.Surname).NotEmpty();
            RuleFor(c => c.Model.BirthDate.Date).NotEmpty();
        }
    }
}

