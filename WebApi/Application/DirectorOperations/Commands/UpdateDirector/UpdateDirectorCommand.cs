using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommand
    {
        public int DirectorId { get; set; }
        public UpdateDirectorViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateDirectorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var Director = _dbContext.Directors.SingleOrDefault(a => a.Id == DirectorId);

            if(Director is null)
                throw new InvalidOperationException("The director you tried to update could not be found.");

            Director.Name = Model.Name == default ? Director.Name : Model.Name;
            Director.Surname = Model.Surname == default ? Director.Surname : Model.Surname;

            _dbContext.SaveChanges();
            
        }
    }

    public class UpdateDirectorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}