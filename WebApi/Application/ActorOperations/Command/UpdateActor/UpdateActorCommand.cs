using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DBOperations;

namespace WebApi.Application.ActorOperations.Command.UpdateActor
{
    public class UpdateActorCommand
    {
         public int ActorId { get; set; }
        public UpdateActorViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateActorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var actor = _dbContext.Actors.SingleOrDefault(x => x.Id == ActorId);

            if (actor is null)
                throw new InvalidOperationException("The actor you tried to update was not found.");

            actor.Name = Model.Name == default ? actor.Name : Model.Name;
            actor.Surname = Model.Surname == default ? actor.Surname : Model.Surname;
             

           _dbContext.SaveChanges();
            
        }
    }

    public class UpdateActorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}