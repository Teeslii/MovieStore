using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.UnitTests.TestSetup;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using WebApi.Application.DirectorOperations.Commands.UpdateDirector;

namespace Tests.WebApi.UnitTests.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public UpdateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.context;
        }

        [Fact]
        public void WhenAlreadyDirectorNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
            command.DirectorId = 999;

            command.Model = new UpdateDirectorViewModel 
            {
                 Name = "Nadine",
                 Surname = "Labaki" 
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("The director you tried to update could not be found.");
        }

        [Fact]
        public void WhenValidInputAreGiven_Director_ShouldBeUpdated()
        {
            var director = new Director
            { 
                Name = "Nadine", 
                Surname = "Labaki" 
            };
            _context.Directors.Add(director);
            _context.SaveChanges();

            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
            int id = _context.Directors.FirstOrDefault(a => a.Name == director.Name && a.Surname == director.Surname).Id; 

            var model = new UpdateDirectorViewModel
            {
                 Name="NadineTest",
                 Surname="LabakiTest"
            };
            command.DirectorId = id;
            command.Model = model;

            FluentActions
                .Invoking(()=>command.Handle()).Invoke();

            var updatedDirector = _context.Directors.FirstOrDefault(Director => Director.Name == model.Name && Director.Surname == model.Surname);
            
            updatedDirector.Should().NotBeNull();
            updatedDirector.Name.Should().Be(model.Name);
            updatedDirector.Surname.Should().Be(model.Surname);

        }
    }

}