using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.CustomerOperations.Queries.GetCustomerDetail;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.CustomerOperations.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQueryValidatorTest  
    {
        [Theory]
        [InlineData(0)] 
        [InlineData(-1)] 
        public void WhenNonPositiveIdIsGiven_Validator_ShouldReturnError(int id)
        {
        
            GetCustomerDetailQuery query = new GetCustomerDetailQuery(null, null);
            query.CustomerId = id;

            
            GetCustomerDetailQueryValidator validator = new GetCustomerDetailQueryValidator();
            var validationResult = validator.Validate(query);

          
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenPositiveIdIsGiven_Validator_ShouldNotReturnError()
        {
      
            GetCustomerDetailQuery query = new GetCustomerDetailQuery(null, null);
            query.CustomerId = 1;

            GetCustomerDetailQueryValidator validator = new GetCustomerDetailQueryValidator();
            var validationResult = validator.Validate(query);

           
            validationResult.Errors.Count.Should().Be(0);
        }

    }
}