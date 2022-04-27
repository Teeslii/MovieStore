using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.Command.CreateActor
{
    public class CreateActorCommand
    {
        public CreateActorViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateActorCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle(){
            var actor = _dbContext.Actors.SingleOrDefault(a => a.Name == Model.Name && a.Surname == Model.Surname);

            if(actor is not null)
                throw new InvalidOperationException("The actor you are trying to add already exists.");
                
            actor = _mapper.Map<Actor>(Model);   
            _dbContext.Actors.Add(actor);
            _dbContext.SaveChanges();
        }
    }
    public class CreateActorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}