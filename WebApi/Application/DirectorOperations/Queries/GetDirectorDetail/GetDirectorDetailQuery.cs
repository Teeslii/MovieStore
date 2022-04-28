using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorDetailQuery
    {
        public int DirectorId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDirectorDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public DirectorDetailViewModel Handle()
        {
            var director = _dbContext.Directors.Include(a => a.Movies).SingleOrDefault(x => x.Id == DirectorId);

            if(director is null)
                throw new InvalidOperationException("The director you are looking for could not be found.");

            DirectorDetailViewModel returnObj = _mapper.Map<DirectorDetailViewModel>(director);
            
            return returnObj;
        }
    }

    public class DirectorDetailViewModel
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        
        public List<DirectorMoviesViewModel> Movies { get; set; }

        public struct DirectorMoviesViewModel
        {
            public int Id { get; set; }
            public string Title { get; set; }   
        }
    }
}