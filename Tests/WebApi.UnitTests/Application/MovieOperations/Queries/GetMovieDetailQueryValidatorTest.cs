using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.MovieOperations.Queries
{
    public class GetMovieDetailQueryValidatorTest  
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidMovieIdIsGiven_Validator_ShouldBeReturnErrors(int Id)
        {
            GetMovieDetailQuery query = new GetMovieDetailQuery(null, null);
             query.MovieId = Id;

             GetMovieDetailQueryValidator validator = new GetMovieDetailQueryValidator();

             var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidGenreIdIsGiven_Validator_ShouldNotReturnError()
        {
             GetMovieDetailQuery query = new GetMovieDetailQuery(null, null);
             query.MovieId = 1;

             GetMovieDetailQueryValidator validator = new GetMovieDetailQueryValidator();

             var result = validator.Validate(query);

            result.Errors.Count.Should().Be(0);
        }    

    }
}