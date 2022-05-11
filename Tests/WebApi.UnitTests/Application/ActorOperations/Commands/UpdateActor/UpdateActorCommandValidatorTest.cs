
using FluentAssertions;
using WebApi.UnitTests.TestSetup;
using WebApi.Application.ActorOperations.Command.UpdateActor;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,"a","b")]
        [InlineData(0,"a","bc")]
        [InlineData(0,"ac","b")]
        [InlineData(0,"ab"," ")]
        [InlineData(0," ","ba")]
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
        public void WhenInvalidActorIdIsGiven_Validator_ShouldBeReturnErrors(int actorId,string name,string surname)
        {
            UpdateActorCommand commad = new UpdateActorCommand(null);
            commad.ActorId = actorId;
            commad.Model = new UpdateActorViewModel
            {
                Name = name,
                Surname = surname
            };
            UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            var result = validator.Validate(commad);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
           
        [Fact]
       public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
       {
         UpdateActorCommand command = new UpdateActorCommand(null);
         command.Model = new UpdateActorViewModel()
         {
             Name = "Richard",
             Surname = "Ayoade"
         };

         UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
         var result = validator.Validate(command);

         result.Errors.Count.Should().BeGreaterThan(0);
       }
    }

}