using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.UnitTests.TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var genre = new Genre 
            { 
                Name = "Animation" 
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            command.Model = new CreateGenreViewModel
            { 
                Name = genre.Name
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("The type of movie you are trying to add already exists.");
        }

        [Fact]
        public void WhenValidInputAreGiven_Genre_ShouldBeCreated()
        {
            CreateGenreCommand command = new CreateGenreCommand(_context,_mapper);
            var model = new CreateGenreViewModel 
            { 
                Name="Animation"
            };
            command.Model = model;

            FluentActions
                .Invoking(()=>command.Handle()).Invoke();

            var genre = _context.Genres.FirstOrDefault(genre => genre.Name == model.Name);
            
            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);

        }
    }

}