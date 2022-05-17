using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.UnitTests.TestSetup;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetMovieDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotFoundMovieIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            int id = 999;

            GetMovieDetailQuery query = new GetMovieDetailQuery(_context,_mapper);
            query.MovieId = id;

            FluentActions
                    .Invoking(() => query.Handle())
                    .Should().Throw<InvalidOperationException>()
                    .And.Message.Should().Be("The movie you were looking for was not found.");
        }

        [Fact]
        public void WhenValidMovieIdIsGiven_Movie_ShouldBeReturn()
        {
            var newMovie = new Movie 
            {
                Title="Songs My Brothers Taught Me", 
                ReleaseYear = 2016, 
                DirectorId = 4,
                GenreId = 3, 
                Price = 146
            };
            _context.Movies.Add(newMovie);
            _context.SaveChanges();

            var Movie = _context.Movies.SingleOrDefault(a => a.Title == newMovie.Title );

            GetMovieDetailQuery query = new GetMovieDetailQuery(_context,_mapper);
            query.MovieId = Movie.Id;

            FluentActions.Invoking(() => query.Handle()).Invoke();

            var findMovie = _context.Movies.SingleOrDefault(a => a.Id == Movie.Id);

            findMovie.Should().NotBeNull();
        }

    }
}
