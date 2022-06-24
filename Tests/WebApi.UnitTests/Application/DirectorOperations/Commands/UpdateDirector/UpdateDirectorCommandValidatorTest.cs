
using FluentAssertions;
using WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommandValidatorTest  
    {
        [Theory]
        [InlineData(0,"a","b")]
        [InlineData(0,"a","bc")]
        [InlineData(0,"ac","b")]
        [InlineData(0,"ac"," ")]
        [InlineData(0," ","bc")]
        [InlineData(0," ","b")]
        [InlineData(0,"a"," ")]
        [InlineData(-1,"a","b")]
        [InlineData(-1,"a","bc")]
        [InlineData(-1,"ac","b")]
        [InlineData(-1,"ac"," ")]
        [InlineData(-1," ","bc")]
        [InlineData(-1," ","b")]
        [InlineData(-1,"a"," ")]
        [InlineData(1,"a","b")]
        [InlineData(1,"a","bc")]
        [InlineData(1,"ac","b")]
        [InlineData(1," ","b")]
        [InlineData(1,"a"," ")]
        public void WhenInvalidDirectorIdIsGiven_Validator_ShouldBeReturnErrors(int Id, string name, string surname)
        {
            UpdateDirectorCommand commad = new UpdateDirectorCommand(null);
            commad.DirectorId = Id;

            commad.Model = new UpdateDirectorViewModel
            {
                Name = name,
                Surname = surname
            };
            UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
            var result = validator.Validate(commad);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1,"aa","bb")]
        [InlineData(1," "," ")]
        [InlineData(1,"aa"," ")]
        [InlineData(1," ","bb")]
        public void WhenValidDirectorIdIsGiven_Validator_ShouldNotReturnError(int Id, string name, string surname)
        {
            UpdateDirectorCommand commad = new UpdateDirectorCommand(null);
            commad.DirectorId = Id;
            commad.Model = new UpdateDirectorViewModel
            {
                Name = name,
                Surname = surname  
            };
        
            UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
            var result = validator.Validate(commad);

            result.Errors.Count.Should().Be(0);
        }

    }

}