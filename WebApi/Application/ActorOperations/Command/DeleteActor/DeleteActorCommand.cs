using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DBOperations;

namespace WebApi.Application.ActorOperations.Command.DeleteActor
{
    public class DeleteActorCommand
    {
        public int ActorId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteActorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var actor = _dbContext.Actors.SingleOrDefault(a => a.Id == ActorId);

            if(actor is null)
                throw new InvalidOperationException("The actor you tried to delete was not found.");
          
            var movieActors = _dbContext.MovieOfActors.Any(x => x.ActorId == ActorId);
            
            if(movieActors)
                throw new InvalidOperationException("An actor with a movie cannot be deleted! First you have to delete the movies of the actor.");
            
            _dbContext.Actors.Remove(actor);            
            _dbContext.SaveChanges();      
        }
    }
}