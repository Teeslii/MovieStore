using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.UnitTests.TestSetup;
using WebApi.Application.MovieOperations.Command.UpdateMovie;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommandTests : IDisposable, IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public UpdateMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void WhenAlreadyMovieNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            UpdateMovieCommand command = new UpdateMovieCommand(_context);
            command.MovieId = 999;
            command.Model = new UpdateMovieViewModel 
            {
                Title = "Songs My Brothers Taught MeTest", 
                ReleaseYear = 2000, 
                DirectorId = 4,
                GenreId = 3, 
                Price = 1464
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("The movie you are trying to update could not be found.");
        }

        [Fact]
        public void WhenValidInputAreGiven_Movie_ShouldBeUpdated()
        {
             var Movie = new Movie
             {
                Title = "Songs My Brothers Taught Me", 
                ReleaseYear = 2016, 
                DirectorId = 4,
                GenreId = 3, 
                Price = 146
             };
            _context.Movies.Add(Movie);
            _context.SaveChanges();

            UpdateMovieCommand command = new UpdateMovieCommand(_context);
            int id = _context.Movies.FirstOrDefault(a => a.Title == Movie.Title ).Id;
            var model = new UpdateMovieViewModel 
            {
                Title = "Songs My Brothers Taught MeTest", 
                ReleaseYear = 2000, 
                DirectorId = 4,
                GenreId = 3, 
                Price = 1464
            };
            command.MovieId = id;
            command.Model = model;

            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            var updatedMovie = _context.Movies.FirstOrDefault(Movie => Movie.Title == model.Title);

            updatedMovie.Should().NotBeNull();
            updatedMovie.Title.Should().Be(model.Title);
            updatedMovie.DirectorId.Should().Be(model.DirectorId);
            updatedMovie.GenreId.Should().Be(model.GenreId);
            updatedMovie.Price.Should().Be(model.Price);
        }
    }

}