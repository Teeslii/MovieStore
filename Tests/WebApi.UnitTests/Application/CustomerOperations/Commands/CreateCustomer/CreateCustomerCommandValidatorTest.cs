using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MovieStore.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidatorTest  
    {

        [Theory]
        [InlineData(" ", "july123", "July", "Morning")]
        [InlineData("  xxx   ", "july123", "July", "Morning")]
        [InlineData("in%va%lid%email.com", "july123", "July", "Morning")]
        [InlineData("oda.com", "july123", "July", "Morning")]
        [InlineData("A@b@c@outlook.com", "july123", "July", "Morning")]
        [InlineData("julyM@outlook.com", "a", "July", "Morning")]
        [InlineData("julyM@outlook.com", "ab", "July", "Morning")]
        [InlineData("julyM@outlook.com", "abc", "July", "Morning")]
        [InlineData("julyM@outlook.com", "abcd", "July", "Morning")]
        [InlineData("julyM@outlook.com", "abcde", "July", "Morning")]
        [InlineData("julyM@outlook.com", "july123", "", "Morning")]
        [InlineData("julyM@outlook.com", "july123", " ", "Morning")]
        [InlineData("julyM@outlook.com", "", "July", "Morning")]
        [InlineData("julyM@outlook.com", " ", "July", "Morning")]
        [InlineData("julyM@outlook.com", "   ", "July", "Morning")]
        [InlineData("julyM@outlook.com", "july123", "July", "")]
        [InlineData("julyM@outlook.com", "july123", "July", " ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string email, string password, string name, string surname)
        {
        
                CreateCustomerCommand command = new CreateCustomerCommand(null, null);
                command.Model = new CreateCustomerModel()
                {
                    Email = email,
                    Password = password,
                    Name = name,
                    Surname = surname
                };

                CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
                var validationResult = validator.Validate(command);

                validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
                CreateCustomerCommand command = new CreateCustomerCommand(null, null);
                command.Model = new CreateCustomerModel()
                {
                    Email = "julyM@oda.co",
                    Password = "july123",
                    Name = "July",
                    Surname = "Morning"
                };

                CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
                var validationResult = validator.Validate(command);
                
                validationResult.Errors.Count.Should().Be(0);
        }
    }
}