using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.ActorOperations.Queries.GetActors
{
    public class GetActorsQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetActorsQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<ActorsViewModel> Handle()
        {
            var actors = _dbContext.Actors.Include(a => a.MovieOfActors).ThenInclude(ma => ma.Movie).OrderBy(x => x.Id);

            List<ActorsViewModel> returnObj = _mapper.Map<List<ActorsViewModel>>(actors);
            
            return returnObj;
        }

    }

    public class ActorsViewModel
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public List<ActorMoviesViewModel> Movies { get; set; }

        public struct ActorMoviesViewModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
        }
    }
}