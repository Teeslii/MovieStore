using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Command.CreateMovie
{
    public class CreateMovieCommand
    {
         public CreateMovieViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateMovieCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

         public void Handle()
        {
            var movie = _dbContext.Movies.SingleOrDefault(m => m.Title == Model.Title && m.ReleaseYear == Model.ReleaseYear);
            if(movie is not null)
                throw new InvalidOperationException("The movie you are trying to add already exists.");

            movie = _mapper.Map<Movie>(Model);
            _dbContext.Movies.Add(movie);
            
            _dbContext.SaveChanges();
            
        }
    }

    public class  CreateMovieViewModel 
    {
         public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public decimal Price { get; set; }  
    }
}