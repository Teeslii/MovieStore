using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.MovieOfActorOperations.Queries.GetActorMovies
{
    public class GetActorMoviesQuery
    {
        public int ActorId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetActorMoviesQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<ActorOfMoviesViewModel> Handle()
        {
            var movies = _dbContext.MovieOfActors.Include(x => x.Movie).ThenInclude(x => x.Genre).Include(x => x.Movie.Director).Where(ma => ma.ActorId == ActorId).ToList();
           
            if (movies is null)
                throw new InvalidOperationException("Films of the actor not found.");

            List<ActorOfMoviesViewModel> actorMovies = _mapper.Map<List<ActorOfMoviesViewModel>>(movies);
            
            return actorMovies;
        }
    }
      public class ActorOfMoviesViewModel
        {
            public int MovieId { get; set; }
            public string Title { get; set; }
            public int ReleaseYear { get; set; }
            public string Genre { get; set; }
            public string Director { get; set; }
        }
}