using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.UnitTests.TestSetup;
using Xunit;
using FluentAssertions;
using WebApi.DBOperations;
using AutoMapper;
using WebApi.Entities;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.CreateCustomer
{
    public class CreateCustomerCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateCustomerCommandTest(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistingCustomerEmailIsGiven_Handle_ThrowsInvalidOperationException()
        {
            var customer = new Customer()
            {
                Email = "julyM@oda.co",
                Password = "july123",
                Name = "July",
                Surname = "Morning"

            };
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

           
            CreateCustomerCommand command = new CreateCustomerCommand(_dbContext, _mapper);
            command.Model = new CreateCustomerModel()
            {
                Email = "julyM@oda.co",
                Password = "july123",
                Name = "July",
                Surname = "Morning"
            };

           
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And
                .Message.Should().Be("The user you are trying to add already exists.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Customer_ShouldBeCreated()
        {
            
            CreateCustomerCommand command = new CreateCustomerCommand(_dbContext, _mapper);
            var model = new CreateCustomerModel()
            {
                Email = "julyM@oda.co",
                Password = "july123",
                Name = "July",
                Surname = "Morning"
            };
            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var customer = _dbContext.Customers.SingleOrDefault(customer => customer.Email.ToLower() == model.Email.ToLower());

            customer.Should().NotBeNull();
        }
    }
}