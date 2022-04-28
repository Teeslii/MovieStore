using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.MovieOfActorOperations.Queries.GetMovieActors
{
    public class GetMovieActorsQuery
    {
        public int MovieId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetMovieActorsQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        
        public List<MovieOfActorsViewModel> Handle()
        {
            var actors = _dbContext.MovieOfActors.Include(x => x.Actor).Where(ma => ma.MovieId == MovieId).ToList();

            if(actors is null)
                throw new InvalidOperationException("The actors of the movie could not be found.");
            
            List<MovieOfActorsViewModel> movieActors = _mapper.Map<List<MovieOfActorsViewModel>>(actors);

            return movieActors;  
        }

    }
     public class MovieOfActorsViewModel
     {
            public int ActorId { get; set; }
            public string NameSurname { get; set; }
            
     }
}