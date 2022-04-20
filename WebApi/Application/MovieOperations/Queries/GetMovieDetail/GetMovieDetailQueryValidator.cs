using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace WebApi.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQueryValidator : AbstractValidator<GetMovieDetailQuery>
    {
         public GetMovieDetailQueryValidator()
        {
            RuleFor(query => query.MovieId).GreaterThan(0);
        }
    }
}