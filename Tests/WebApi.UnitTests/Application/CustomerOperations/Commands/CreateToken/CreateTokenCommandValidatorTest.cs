using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MovieStore.Application.CustomerOperations.Commands.CreateToken;
using WebApi.Application.CustomerOperations.Commands.CreateToken;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.CustomerOperations.Commands.CreateToken
{
    public class  CreateTokenCommandValidatorTest 
    {
        [Theory]
        [InlineData("", "timrayne123")]
        [InlineData(" ", "timrayne123")]
        [InlineData("    ", "timrayne123")]
        [InlineData("  ray   ", "timrayne123")]
        [InlineData("timrayne@outlook.com", "")]
        [InlineData("timrayne@outlook.com", " ")]
        [InlineData("timrayne@outlook.com", "   ")]
        [InlineData("in%va%lid%outlook.com", "timrayne123")]
        [InlineData("outlook.com", "timrayne123")]
        [InlineData("t@i@m@outlook.com", "timrayne123")]
        [InlineData("timrayne@outlook.com", "t")]
        [InlineData("timrayne@outlook.com", "ti")]
        [InlineData("timrayne@outlook.com", "tim")]
        [InlineData("timrayne@outlook.com", "timr")]
        [InlineData("timrayne@outlook.com", "timra")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string email, string password)
        {
    
                CreateTokenCommand command = new CreateTokenCommand(null, null, null);
                command.Model = new CreateTokenModel()
                {
                    Email = email,
                    Password = password
                };

            
                CreateTokenCommandValidator validator = new CreateTokenCommandValidator();
                var validationResult = validator.Validate(command);

                validationResult.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
               
                CreateTokenCommand command = new CreateTokenCommand(null, null, null);
                command.Model = new CreateTokenModel()
                {
                    Email = "timrayne@outlook.com",
                    Password = "timrayne123"
                };

                CreateTokenCommandValidator validator = new CreateTokenCommandValidator();
                var validationResult = validator.Validate(command);

                validationResult.Errors.Count.Should().Be(0);
        }
    }
}