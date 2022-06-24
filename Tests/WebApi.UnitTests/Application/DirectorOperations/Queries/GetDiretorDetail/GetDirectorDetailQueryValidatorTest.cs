using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.DirectorOperations.Queries.GetDiretorDetail
{
    public class GetDirectorDetailQueryValidatorTest  
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidDirectorIdIsGiven_Validator_ShouldBeReturnErrors(int Id)
        {
            GetDirectorDetailQuery commad = new GetDirectorDetailQuery(null, null);
            commad.DirectorId = Id;
            
            GetDirectorDetailQueryValidator validator = new GetDirectorDetailQueryValidator();
            var result = validator.Validate(commad);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidDirectorIdIsGiven_Validator_ShouldNotReturnError()
        {
            GetDirectorDetailQuery commad = new GetDirectorDetailQuery(null, null);
            commad.DirectorId = 1;
           
            GetDirectorDetailQueryValidator validator = new GetDirectorDetailQueryValidator();
            var result = validator.Validate(commad);

            result.Errors.Count.Should().Be(0);
        }
    }
}