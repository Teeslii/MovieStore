using System.Linq;
using WebApi.DBOperations;
using System;

namespace WebApi.Application.MovieActorOperations.Commands.DeleteMovieActor
{
    public class DeleteMovieActorCommand
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        public DeleteMovieActorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public void Handle()
        {
            var movieActor = _dbContext.MovieOfActors.SingleOrDefault(ma => ma.MovieId == MovieId && ma.ActorId == ActorId);

            if(movieActor is null)
                throw new InvalidOperationException("No defined actor found in the movie.");

            _dbContext.MovieOfActors.Remove(movieActor);
            _dbContext.SaveChanges();
               
        }
    }
}