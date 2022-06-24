using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.Application.CustomerOperations.Commands.CreateToken;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.CreateToken
{
    public class  CreateTokenCommandTest : IDisposable, IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateTokenCommandTest(CommonTestFixture testFixture)
        {
             _dbContext = testFixture.context;
             _mapper = testFixture.Mapper;
             _configuration = testFixture.Configuration;
        }

          public void Dispose()
        {
            _dbContext.Dispose();
        }
        
        [Theory]
        [InlineData("timrayne@outlook.com", "timrayne123")]
        [InlineData("anniermey@outlook.com", "anniermey123")]
        [InlineData("laurieaste@outlook.com", "laurieaste123")]
        public void WhenGivenCredentialsAreInvalid_Handle_ThrowsInvalidOperationException(string email, string password)
        {
         
                CreateTokenCommand command = new CreateTokenCommand(_dbContext, _mapper, _configuration);
                command.Model = new CreateTokenModel()
                {
                    Email = email,
                    Password = password,
                };

                FluentActions
                    .Invoking(() => command.Handle())
                    .Should().Throw<InvalidOperationException>()
                    .And
                    .Message.Should().Be("E-mail address and password are incorrect.");
        }
        
        [Fact]
        public void WhenValidInputsAreGiven_Customer_ShouldBeCreated()
        {
        
                var customerModel = new CreateCustomerModel()
                {
                    Name = "Spencer",
                    Surname = "Bradley",
                    Email = "spencerbradley@outlook.com",
                    Password = "Spencer123"
                };

                var customer = _mapper.Map<Customer>(customerModel);
                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();

                CreateTokenCommand command = new CreateTokenCommand(_dbContext, _mapper, _configuration);
                command.Model = new CreateTokenModel()
                {
                    Email = customerModel.Email,
                    Password = customerModel.Password,
                };

                var token = command.Handle();

                token.Should().NotBeNull();
                token.AccessToken.Should().NotBeNull();
                token.RefreshToken.Should().NotBeNull();
                token.Expiration.Should().BeAfter(DateTime.Now);

        }
    }
}