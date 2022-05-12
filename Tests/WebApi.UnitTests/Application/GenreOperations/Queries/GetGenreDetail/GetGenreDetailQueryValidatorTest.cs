using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidGenreIdIsGiven_Validator_ShouldBeReturnErrors(int Id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(null,  null);
            query.GenreId = Id;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();

            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }    
        
        [Fact]
        public void WhenValidGenreIdIsGiven_Validator_ShouldNotReturnError()
        {
               GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
               query.GenreId = 1;

               GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
               var result = validator.Validate(query);

               result.Errors.Count.Should().Be(0);
        }    

    }
}