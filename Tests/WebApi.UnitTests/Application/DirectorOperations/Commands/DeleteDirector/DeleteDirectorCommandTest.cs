using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.UnitTests.TestSetup;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using WebApi.Application.DirectorOperations.Commands.DeleteDirector;

namespace Tests.WebApi.UnitTests.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteDirectorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotFoundDirectorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            DeleteDirectorCommand command = new DeleteDirectorCommand(_context);  
            command.DirectorId = 999;

            FluentActions
                    .Invoking(() => command.Handle())
                    .Should().Throw<InvalidOperationException>()
                    .And.Message.Should().Be("The director you tried to delete was not found.");

        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void WhenDirectorHasAMovie_InvalidOperationException_ShouldBeReturnErrors(int Id)
        {
                DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
                command.DirectorId = Id;

                FluentActions
                        .Invoking(() => command.Handle())
                        .Should().Throw<InvalidOperationException>()
                        .And.Message.Should().Be("The director must be deleted from the movie first.");
        }    

        [Fact]
        public void WhenValidDirectorIdIsGiven_Director_ShouldBeDeleted()
        {
            var newDirector = new Director
            {
                 Name="Nadine", 
                 Surname="Labaki"
            };
            _context.Directors.Add(newDirector);
            _context.SaveChanges();

            var Director = _context.Directors.SingleOrDefault(a => a.Name==newDirector.Name && a.Surname==newDirector.Surname);

            DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
            command.DirectorId = Director.Id;

            FluentActions
                    .Invoking(() => command.Handle()).Invoke();

            var findDirector = _context.Directors.SingleOrDefault(a => a.Name == newDirector.Name && a.Surname == newDirector.Surname);
            findDirector.Should().BeNull();

        }

    }
}
