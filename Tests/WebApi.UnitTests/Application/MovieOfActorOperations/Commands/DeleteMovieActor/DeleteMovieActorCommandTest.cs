using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.UnitTests.TestSetup;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using WebApi.Application.MovieActorOperations.Commands.DeleteMovieActor;

namespace Tests.WebApi.UnitTests.Application.MovieOfActorOperations.Commands.DeleteMovieActor
{
    public class DeleteMovieActorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public DeleteMovieActorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.context;
        }

        [Fact]
        public void WhenNotFoundActorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var movieId = 3;
            var actorId = 9999;

            DeleteMovieActorCommand command = new DeleteMovieActorCommand(_context);
            command.ActorId = actorId;
            command.MovieId = movieId;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("No defined actor found in the movie.");
        }

        [Fact]
        public void WhenValidInputAreGiven_MovieActor_ShouldBeDeleted()
        {
            var movie = new Movie
            { 
                Title="Songs My Brothers Taught Me", 
                ReleaseYear = 2016, 
                DirectorId = 4,
                GenreId = 3, 
                Price = 146
            };
            var actor = new Actor 
            {
                Name = "John",
                Surname = "Reddy"
            };
            _context.Movies.Add(movie);
            _context.Actors.Add(actor);
            _context.SaveChanges();

            var movieId = _context.Movies.FirstOrDefault(x => x.Title == movie.Title).Id;
            var actorId = _context.Actors.FirstOrDefault(x => x.Name == actor.Name && x.Surname == actor.Surname).Id;

            _context.MovieOfActors.Add(
                        new MovieOfActors
                        { 
                            MovieId = movieId,
                            ActorId = actorId
                        }
            );
            _context.SaveChanges();

            DeleteMovieActorCommand command = new DeleteMovieActorCommand(_context);
            command.ActorId = actorId;
            command.MovieId = movieId;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var movieActor = _context.MovieOfActors.FirstOrDefault(x => x.MovieId == movieId && x.ActorId == actorId);
            movieActor.Should().BeNull();
        }

    }

}