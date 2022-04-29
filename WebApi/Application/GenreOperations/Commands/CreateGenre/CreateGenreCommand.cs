using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    { 
        public CreateGenreViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateGenreCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Name.ToLower() == Model.Name.ToLower());
            
            if(genre is not null)
                throw new InvalidOperationException("The type of movie you are trying to add already exists.");
           
            genre = _mapper.Map<Genre>(Model);
          
            _dbContext.Genres.Add(genre);
           
            _dbContext.SaveChanges();
        }
    }

    public class CreateGenreViewModel
    {
        public string Name { get; set; }
    }
}