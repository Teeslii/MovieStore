using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.Application.CustomerOperations.Commands.CreateToken;
using WebApi.Application.CustomerOperations.Commands.RefreshToken;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.RefreshToken
{
    public class RefreshTokenCommandTest : IDisposable, IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public RefreshTokenCommandTest(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.context;
            _configuration = testFixture.Configuration;
            _mapper = testFixture.Mapper;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        [Fact]
        public void WhenValidInputsAreGiven_AccessToken_ShouldBeCreatedWithRefreshToken()
        {
      
                CreateCustomerModel customerModel = new CreateCustomerModel()
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
                    Password = customerModel.Password
                };

                var token = command.Handle();

                RefreshTokenCommand refreshTokenCommand = new RefreshTokenCommand(_dbContext, _configuration);
                refreshTokenCommand.RefreshToken = token.RefreshToken;

                var newToken = refreshTokenCommand.Handle();

                newToken.Should().NotBeNull();
                newToken.AccessToken.Should().NotBeNull();
                newToken.RefreshToken.Should().NotBeNull();
                newToken.RefreshToken.Should().NotBe(token.RefreshToken);
                newToken.Expiration.Should().BeAfter(token.Expiration);
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_Handle_ThrowsInvalidOperationException()
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_dbContext, _configuration);
            command.RefreshToken = "fake refresh token";

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And
                .Message.Should().Be("A valid refresh token was not found.");
        }

    }
}