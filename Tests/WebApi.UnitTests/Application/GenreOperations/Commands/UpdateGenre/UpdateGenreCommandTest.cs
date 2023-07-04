using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.UnitTests.TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests :  IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.context;
        }

        [Fact]
        public void WhenAlreadyExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 999;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("The movie type you are trying to update could not be found.");
        }


        [Fact]
        public void WhenValidInputAreGiven_Genre_ShouldBeUpdated()
        {
            var genre = new Genre 
            { 
                Name = "Animation"
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            int id = _context.Genres.FirstOrDefault(a => a.Name == genre.Name ).Id;

            var model = new UpdateGenreModel { Name="AnimationTest"};
            command.GenreId = id;
            command.Model = model;

            FluentActions
                .Invoking(()=>command.Handle()).Invoke();

            var updatedGenre = _context.Genres.FirstOrDefault(genre => genre.Name == model.Name);
            
            updatedGenre.Should().NotBeNull();
            updatedGenre.Name.Should().Be(model.Name);

        }
    }

}