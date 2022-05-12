using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommand
    {
        public int DirectorId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteDirectorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var Director = _dbContext.Directors.SingleOrDefault(a => a.Id == DirectorId);
            
            if(Director is null)
                throw new InvalidOperationException("The director you tried to delete was not found.");
           
            var movies = _dbContext.Movies.Any(x=> x.DirectorId == DirectorId);
            
            if(movies)
                throw new InvalidOperationException("The director must be deleted from the movie first.");
            
            _dbContext.Directors.Remove(Director);            
            _dbContext.SaveChanges();
             
        }
    }
}