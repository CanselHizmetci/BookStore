using System;
using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidator:AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(c => c.GenreId).GreaterThan(0);
            RuleFor(c => c.Model.Name).NotEmpty().MinimumLength(4);
        }
    }
}

