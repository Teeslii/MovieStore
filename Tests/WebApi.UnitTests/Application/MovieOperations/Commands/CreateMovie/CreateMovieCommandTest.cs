using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.UnitTests.TestSetup;
using WebApi.Application.MovieOperations.Command.CreateMovie;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommandTests : IDisposable, IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.context;
            _mapper = testFixture.Mapper;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void WhenAlreadyMovieNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var movie = new Movie
            { 
                 Title="Songs My Brothers Taught Me", 
                ReleaseYear = 2016, 
                DirectorId = 4,
                GenreId = 3, 
                Price = 146
            };
            _context.Movies.Add(movie);
            _context.SaveChanges();

            CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
            command.Model = new CreateMovieViewModel 
            { 
                Title = movie.Title,
                ReleaseYear = movie.ReleaseYear,
                DirectorId = movie.DirectorId,
                GenreId = movie.GenreId,
                Price = movie.Price
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("The movie you are trying to add already exists.");
        }

        [Fact]
        public void WhenValidInputAreGiven_Movie_ShouldBeCreated()
        {
            CreateMovieCommand command = new CreateMovieCommand(_context,_mapper);
            var model = new CreateMovieViewModel 
            { 
                Title="Songs My Brothers Taught Me", 
                ReleaseYear = 2016, 
                DirectorId = 4,
                GenreId = 3, 
                Price = 146
            };
            command.Model = model;

            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            var movie = _context.Movies.FirstOrDefault(m => m.Title == model.Title && m.ReleaseYear == model.ReleaseYear);

            movie.Should().NotBeNull();
            movie.Title.Should().Be(model.Title);
            movie.ReleaseYear.Should().Be(model.ReleaseYear);
            movie.DirectorId.Should().Be(model.DirectorId);
            movie.GenreId.Should().Be(model.GenreId);
            movie.Price.Should().Be(model.Price);

        }
    }

}