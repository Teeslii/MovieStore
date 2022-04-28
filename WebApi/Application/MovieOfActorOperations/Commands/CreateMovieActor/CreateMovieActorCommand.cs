using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using System;
using WebApi.Entities;
using WebApi.DBOperations;

namespace WebApi.Application.MovieActorOperations.Commands.CreateMovieActor
{
    public class CreateMovieActorCommand
    {
        public CreateMovieActorViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateMovieActorCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var movie = _dbContext.Movies.Any(m => m.Id == Model.MovieId);

            var actor = _dbContext.Actors.Any(a => a.Id == Model.ActorId);

            var movieActor = _dbContext.MovieOfActors.SingleOrDefault(ma => ma.MovieId == Model.MovieId && ma.ActorId == Model.ActorId);
            
            if(!movie)
                throw new InvalidOperationException("The movie you were looking for was not found.");

            if(!actor)
                throw new InvalidOperationException("The actor was not found.");
            
            if(movieActor is not null)
                throw new InvalidOperationException("The actor is defined in the movie.");
           
            movieActor = _mapper.Map<MovieOfActors>(Model);
          
            _dbContext.MovieOfActors.Add(movieActor);
            _dbContext.SaveChanges();
                
        }
    }


    public class CreateMovieActorViewModel
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }
    }
}