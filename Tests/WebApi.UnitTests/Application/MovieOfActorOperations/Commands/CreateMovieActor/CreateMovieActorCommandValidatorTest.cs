using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.UnitTests.TestSetup;
using WebApi.Entities;
using Xunit;
using WebApi.Application.MovieActorOperations.Commands.CreateMovieActor;

namespace Tests.WebApi.UnitTests.Application.MovieOfActorOperations.Commands.CreateMovieActor
{
    public class CreateMovieActorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(0,0)]
        [InlineData(0,-1)]
        [InlineData(-1,0)]
        [InlineData(-1,-1)]
        [InlineData(-1,1)]
        [InlineData(0,1)]
        [InlineData(1,-1)]
        [InlineData(1,0)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int movieId,int actorId)
        {
            CreateMovieActorCommand command = new CreateMovieActorCommand(null,null);
            command.Model = new CreateMovieActorViewModel
            {
                MovieId = movieId,
                ActorId = actorId
            };

            CreateMovieActorCommandValidator validator = new CreateMovieActorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);

        }


        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotReturnError()
        {
            CreateMovieActorCommand command = new CreateMovieActorCommand(null,null);
            command.Model = new CreateMovieActorViewModel
            {
                MovieId = 4,
                ActorId = 7
            };

            CreateMovieActorCommandValidator validator = new CreateMovieActorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}