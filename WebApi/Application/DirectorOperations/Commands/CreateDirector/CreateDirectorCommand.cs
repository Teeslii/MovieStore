using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommand
    {
        public CreateDirectorViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateDirectorCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var Director = _dbContext.Directors.SingleOrDefault(a => a.Name == Model.Name && a.Surname == Model.Surname);
            
            if(Director is not null)
                throw new InvalidOperationException("The director you are trying to add already exists.");
            
            Director = _mapper.Map<Director>(Model);
            
            _dbContext.Directors.Add(Director);
            _dbContext.SaveChanges();
           
        }
    }

    public class CreateDirectorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}