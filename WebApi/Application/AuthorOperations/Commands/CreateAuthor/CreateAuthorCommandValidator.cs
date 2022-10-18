
using FluentValidation;

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

