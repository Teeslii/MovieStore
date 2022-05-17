using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.UnitTests.TestSetup;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using WebApi.Application.MovieActorOperations.Commands.CreateMovieActor;

namespace Tests.WebApi.UnitTests.Application.MovieOfActorOperations.Commands.CreateMovieActor
{
    public class CreateMovieActorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateMovieActorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotFoundMovieIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var movieId = 9999;
            var actorId = 2;

            CreateMovieActorCommand command = new CreateMovieActorCommand(_context, _mapper);
            command.Model = new CreateMovieActorViewModel { MovieId = movieId , ActorId = actorId};

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("The movie you were looking for was not found.");
        }

        [Fact]
        public void WhenNotFoundActorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var movieId = 3;
            var actorId = 9999;

            CreateMovieActorCommand command = new CreateMovieActorCommand(_context, _mapper);
            command.Model = new CreateMovieActorViewModel { ActorId = actorId ,MovieId = movieId};

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("The actor was not found.");
        }

        [Fact]
        public void WhenFoundInputAreGiven_InvalidOperationException_ShouldBeReturn()
        {
            var movieId = 4;
            var actorId = 7;
            var model = new MovieOfActors 
            { 
                ActorId = actorId,
                MovieId = movieId
            };
            _context.MovieOfActors.Add(model);
            _context.SaveChanges();

            CreateMovieActorCommand command = new CreateMovieActorCommand(_context, _mapper);
            command.Model = new CreateMovieActorViewModel 
            { 
                ActorId = model.ActorId, 
                MovieId = model.MovieId
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("The actor is defined in the movie.");
        }

        [Fact]
        public void WhenValidInputAreGiven_MovieActor_ShouldBeCreated()
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

            CreateMovieActorCommand command = new CreateMovieActorCommand(_context, _mapper);
            command.Model =  new CreateMovieActorViewModel 
            { 
                 ActorId = actorId, 
                 MovieId = movieId 
            };

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var movieActor = _context.MovieOfActors.FirstOrDefault(x => x.MovieId == movieId && x.ActorId == actorId);
            
            movieActor.Should().NotBeNull();
            movieActor.ActorId.Should().Be(actorId);
            movieActor.MovieId.Should().Be(movieId);
        }

    }

}