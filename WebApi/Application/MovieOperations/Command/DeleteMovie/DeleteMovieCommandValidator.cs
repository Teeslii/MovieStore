using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace WebApi.Application.MovieOperations.Command.DeleteMovie
{
    public class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
    {
        public DeleteMovieCommandValidator()
        {
            RuleFor(command => command.MovieId).GreaterThan(0);
        }
    }
}