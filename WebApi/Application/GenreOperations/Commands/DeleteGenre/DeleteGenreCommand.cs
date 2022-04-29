using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    { 
        public int GenreId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteGenreCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);
            
            if(genre is null)
                throw new InvalidOperationException("The type of movie you tried to delete could not be found.");
           
            var movies = _dbContext.Movies.Any(x => x.GenreId == GenreId);
           
            if(movies) 
                throw new InvalidOperationException("Movies of the movie genre must be deleted first.");
            
            _dbContext.Genres.Remove(genre);
           
            _dbContext.SaveChanges();
            
        }
    }
}