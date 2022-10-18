using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator:AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(c => c.AuthorId).GreaterThan(0);
            RuleFor(c => c.Model.Name).NotEmpty().MinimumLength(5);
            RuleFor(c => c.Model.Surname).NotEmpty();
            RuleFor(c => c.Model.BirthDate.Date).NotEmpty();
        }
    }
}

