using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;


namespace WebApi.Application.DirectorOperations.Queries.GetDirectors
{
    public class GetDirectorsQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDirectorsQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<DirectorsViewModel> Handle()
        {
            var Directors = _dbContext.Directors.Include(a=>a.Movies).OrderBy(x => x.Id);

            List<DirectorsViewModel> returnObj = _mapper.Map<List<DirectorsViewModel>>(Directors);
            
            return returnObj;
        }
    }

    public class DirectorsViewModel
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public List<DirectorMoviesVM> Movies { get; set; }

        public struct DirectorMoviesVM
        {
            public int Id { get; set; }
            public string Title { get; set; }   
        }
    }
}