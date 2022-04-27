using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DBOperations;

namespace WebApi.Application.MovieOperations.Command.UpdateMovie
{
    public class UpdateMovieCommand
    {
        public int MovieId { get; set; }
        public UpdateMovieViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateMovieCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var Movie = _dbContext.Movies.SingleOrDefault(a => a.Id == MovieId);

            if(Movie is null)
                throw new InvalidOperationException("The movie you are trying to update could not be found.");
            
            Movie.Title = Model.Title == default ? Movie.Title : Model.Title;
            Movie.ReleaseYear = Model.ReleaseYear == default ? Movie.ReleaseYear : Model.ReleaseYear;
            Movie.GenreId = Model.GenreId == default ? Movie.GenreId : Model.GenreId;
            Movie.DirectorId = Model.DirectorId == default ? Movie.DirectorId : Model.DirectorId;
            Movie.Price = Model.Price == default ? Movie.Price : Model.Price;

            _dbContext.SaveChanges();   
        }
    }
    
    public class UpdateMovieViewModel
    {
        public string Title { get; set; }  
        public int ReleaseYear { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public decimal Price { get; set; }  
    }
}