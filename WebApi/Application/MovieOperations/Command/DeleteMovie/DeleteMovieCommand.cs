using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DBOperations;

namespace WebApi.Application.MovieOperations.Command.DeleteMovie
{
    public class DeleteMovieCommand
    {
        public int MovieId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteMovieCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

         public void Handle()
         {
            var Movie = _dbContext.Movies.SingleOrDefault(a => a.Id == MovieId);
             
            if(Movie is null)
                throw new InvalidOperationException("The movie you tried to delete was not found.");

            var movieActor = _dbContext.MovieOfActors.Any(x => x.MovieId == MovieId);

            if(movieActor)
                throw new InvalidOperationException("Actors belonging to the movie must be deleted first.");
            
            _dbContext.Movies.Remove(Movie);            
           
            _dbContext.SaveChanges();
            
        }
    }
}