using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.UnitTests.TestSetup;
using Xunit;
using FluentAssertions;
using WebApi.DBOperations;
using AutoMapper;
using WebApi.Application.CustomerOperations.Queries.GetCustomerDetail;
using WebApi.Entities;

namespace WebApi.UnitTests.Application.CustomerOperations.Queries
{
    public class GetCustomerDetailQueryTest : IDisposable, IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetCustomerDetailQueryTest(CommonTestFixture testFixture)
        {
              _context = testFixture.context;
              _mapper = testFixture.Mapper;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        
        [Fact]
        public void WhenGivenCustomerIsNotFound_Handle_ThrowsInvalidOperationException()
        {
         
                GetCustomerDetailQuery query = new GetCustomerDetailQuery(_context, _mapper);
                query.CustomerId = 999;
                
                FluentActions
                    .Invoking(() => query.Handle())
                    .Should().Throw<InvalidOperationException>()
                    .And
                    .Message.Should().Be("The user you are looking for was not found.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Customer_ShouldBeReturned()
        {
            GetCustomerDetailQuery query = new GetCustomerDetailQuery(_context, _mapper);
            query.CustomerId = 2;

            FluentActions.Invoking(() => query.Handle()).Invoke();
 
            var customer = _context.Customers.SingleOrDefault(customer => customer.Id == query.CustomerId);

            customer.Should().NotBeNull();
        }

    }
}