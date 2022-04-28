using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace WebApi.Application.MovieOfActorOperations.Queries.GetActorMovies
{
    public class GetActorMoviesQueryValidator  : AbstractValidator<GetActorMoviesQuery>
    {
        public GetActorMoviesQueryValidator()
        {
            RuleFor(query => query.ActorId).GreaterThan(0);
        }
    }
}