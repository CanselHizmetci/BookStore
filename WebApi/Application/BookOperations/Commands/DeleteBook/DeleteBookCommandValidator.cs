
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    [Route("api/[controller]")]
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
      public DeleteBookCommandValidator()
        {
            RuleFor(c => c.BookId).GreaterThan(0);
        }
    }
}

