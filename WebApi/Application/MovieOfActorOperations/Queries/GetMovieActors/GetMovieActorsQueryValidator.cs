using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace WebApi.Application.MovieOfActorOperations.Queries.GetMovieActors
{
    public class GetMovieActorsQueryValidator : AbstractValidator<GetMovieActorsQuery>
    {
        public GetMovieActorsQueryValidator()
        {
            RuleFor(query => query.MovieId).GreaterThan(0);
        }
    }
}