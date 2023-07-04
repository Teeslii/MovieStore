using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.UnitTests.TestSetup;
using WebApi.Application.ActorOperations.Command.UpdateActor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommandTests :  IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public UpdateActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.context;
        }
        
        
        [Fact]
        public void WhenAlreadyActorNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            UpdateActorCommand command = new UpdateActorCommand(_context);
            command.ActorId = 999;

            command.Model = new UpdateActorViewModel
             {
                  Name = "TinaError", 
                  Surname = "FeyError"
             };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("The actor you tried to update was not found.");
        }

        [Fact]
        public void WhenValidInputAreGiven_Actor_ShouldBeUpdated()
        {
            var actor = new Actor 
            {
                 Name = "Charlene", 
                 Surname = "Swankie" 
            };
            _context.Actors.Add(actor);
            _context.SaveChanges();

            UpdateActorCommand command = new UpdateActorCommand(_context);
            int id = _context.Actors.FirstOrDefault(a => a.Name == actor.Name && a.Surname == actor.Surname).Id;

            var model = new UpdateActorViewModel 
            {
                 Name = "CharleneTest", 
                 Surname="SwankieTest"
            };

            command.ActorId = id;
            command.Model = model;

            FluentActions
                .Invoking(()=>command.Handle()).Invoke();

            var updatedActor = _context.Actors.FirstOrDefault(actor => actor.Name == model.Name && actor.Surname == model.Surname);
            updatedActor.Should().NotBeNull();
            updatedActor.Name.Should().Be(model.Name);
            updatedActor.Surname.Should().Be(model.Surname);
        }
    }

}