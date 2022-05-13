using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.UnitTests.TestSetup;
using WebApi.Application.MovieOperations.Command.DeleteMovie;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public DeleteMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.context;
        }

        [Fact]
        public void WhenNotFoundMovieIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var newMovie = new Movie 
            {
                Title = "Songs My Brothers Taught Me", 
                ReleaseYear = 2016, 
                DirectorId = 4,
                GenreId = 3, 
                Price = 146
            };
            _context.Movies.Add(newMovie);
            _context.SaveChanges();

            var movie = _context.Movies.SingleOrDefault(a => a.Title == newMovie.Title);
            _context.Movies.Remove(movie);
            _context.SaveChanges();

            DeleteMovieCommand command = new DeleteMovieCommand(_context);
            command.MovieId = movie.Id;

            FluentActions
                    .Invoking( () => command.Handle())
                    .Should().Throw<InvalidOperationException>()
                    .And.Message.Should().Be("The movie you tried to delete was not found.");

        }

        [Fact]
        public void WhenValidMovieIdIsGiven_Movie_ShouldBeDeleted()
        {
            var newMovie = new Movie 
            {
                Title = "Songs My Brothers Taught Me", 
                ReleaseYear = 2016, 
                DirectorId = 4,
                GenreId = 3, 
                Price = 146
            };
            _context.Movies.Add(newMovie);
            _context.SaveChanges();

            var Movie = _context.Movies.SingleOrDefault(a => a.Title == newMovie.Title);

            DeleteMovieCommand command = new DeleteMovieCommand(_context);
            command.MovieId = Movie.Id;

            FluentActions.Invoking(()=>command.Handle()).Invoke();

            var findMovie = _context.Movies.SingleOrDefault(a => a.Title == newMovie.Title);
            findMovie.Should().BeNull();
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void WhenMovieHasAnActor_InvalidOperationException_ShouldBeReturnErrors(int Id)
        {
           
            DeleteMovieCommand command = new DeleteMovieCommand(_context);
            command.MovieId = Id;

            
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Actors belonging to the movie must be deleted first.");
        }
    }
}
