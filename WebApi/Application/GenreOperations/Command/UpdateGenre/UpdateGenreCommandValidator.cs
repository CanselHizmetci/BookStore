using System;
using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidator:AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(c => c.Model.Name).MinimumLength(4).When(c => c.Model.Name.Trim() != string.Empty);
        }
    }
}

