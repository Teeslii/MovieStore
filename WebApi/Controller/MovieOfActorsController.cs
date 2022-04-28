using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.MovieOfActorOperations.Queries.GetActorMovies;
using WebApi.Application.MovieOfActorOperations.Queries.GetMovieActors;
using WebApi.DBOperations;

namespace WebApi.Controller
{
     [ApiController]
    [Route("[controller]")]
    public class MovieOfActorsController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public MovieOfActorsController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult GetMovieActors(int movieId, int actorId)
        {
            if (actorId == 0)
            {
                GetMovieActorsQuery query = new GetMovieActorsQuery(_context, _mapper);
                query.MovieId = movieId;
                
                GetMovieActorsQueryValidator validator = new GetMovieActorsQueryValidator();

                validator.ValidateAndThrow(query);

                var result = query.Handle();
                return Ok(result);
            }
            else if (movieId == 0)
            {
                GetActorMoviesQuery query = new GetActorMoviesQuery(_context, _mapper);
                query.ActorId = actorId;

                GetActorMoviesQueryValidator validator = new GetActorMoviesQueryValidator();

                validator.ValidateAndThrow(query);

                var result = query.Handle();
                return Ok(result);
            }
            else
            {
                return BadRequest("ActorId or MovieId must be entered.");
            }

        }
       
    }
}