using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.UnitTests.TestSetup;
using Xunit;
using WebApi.Application.DirectorOperations.Commands.CreateDirector;

namespace Tests.WebApi.UnitTests.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("a","b")]
        [InlineData("a","bc")]
        [InlineData("ac","b")]
        [InlineData("ac"," ")]
        [InlineData(" ","bc")]
        [InlineData(" ","b")]
        [InlineData("a"," ")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string name,string surname)
        {
            CreateDirectorCommand command = new CreateDirectorCommand(null,null);
            command.Model = new CreateDirectorViewModel
            {
                Name = name,
                Surname = surname
            };

            CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotReturnError()
        {
            CreateDirectorCommand command = new CreateDirectorCommand(null,null);
            command.Model = new CreateDirectorViewModel
            {
                Name = "Nadine",
                Surname = "Labaki"
            };

            CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}