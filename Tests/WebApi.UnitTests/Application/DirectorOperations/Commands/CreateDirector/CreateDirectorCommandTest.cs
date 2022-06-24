using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.UnitTests.TestSetup;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using WebApi.Application.DirectorOperations.Commands.CreateDirector;

namespace Tests.WebApi.UnitTests.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommandTests : IDisposable, IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.context;
            _mapper = testFixture.Mapper;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void WhenAlreadyDirectorNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var director = new Director 
            { 
                Name = "Nadine", 
                Surname = "Labaki" 
            };
            _context.Directors.Add(director);
            _context.SaveChanges();

            CreateDirectorCommand command = new CreateDirectorCommand(_context, _mapper);
            command.Model = new CreateDirectorViewModel 
            { 
                Name = director.Name,
                Surname = director.Surname 
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("The director you are trying to add already exists.");
        }

        [Fact]
        public void WhenValidInputAreGiven_Director_ShouldBeCreated(){
            CreateDirectorCommand command = new CreateDirectorCommand(_context,_mapper);
            var model = new CreateDirectorViewModel 
            { 
                Name="Nadine",
                Surname="Labaki"
            };
            command.Model = model;

            FluentActions
                .Invoking(()=>command.Handle()).Invoke();

            var director = _context.Directors.FirstOrDefault(Director => Director.Name == model.Name && Director.Surname == model.Surname);

            director.Should().NotBeNull();
            director.Name.Should().Be(model.Name);
            director.Surname.Should().Be(model.Surname);
        }
    }

}