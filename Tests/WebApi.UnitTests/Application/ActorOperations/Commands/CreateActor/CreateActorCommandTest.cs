using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.ActorOperations.Command.CreateActor;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommandTests : IDisposable, IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.context;
            _mapper = testFixture.Mapper;
        }

         public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void WhenAlreadyActorNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var actor = new Actor 
            {
                 Name = "Violet",
                 Surname = "McGraw"
            };
            _context.Actors.Add(actor);
            _context.SaveChanges();

            CreateActorCommand command = new CreateActorCommand(_context, _mapper);
            command.Model = new CreateActorViewModel 
            { 
                Name = actor.Name, 
                Surname = actor.Surname 
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("The actor you are trying to add already exists.");
        }

        [Fact]
        public void WhenValidInputAreGiven_Actor_ShouldBeCreated()
        {
            CreateActorCommand command = new CreateActorCommand(_context,_mapper);
            var model = new CreateActorViewModel 
            {
                 Name = "Rupert",
                 Surname = "Friend"
            };
            command.Model = model;

            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            var actor = _context.Actors.FirstOrDefault(actor => actor.Name == model.Name && actor.Surname == model.Surname);

            actor.Should().NotBeNull();
            actor.Name.Should().Be(model.Name);
            actor.Surname.Should().Be(model.Surname);

        }
      
    }

}