using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.ActorOperations.Queries.GetActors;
using WebApi.DBOperations;

namespace WebApi.Controller
{
    
    [ApiController]
    [Route("[controller]")]
    public class ActorController : ControllerBase
    {
         private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public ActorController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetActors()
        {
            GetActorsQuery query = new GetActorsQuery(_context, _mapper);

            var result = query.Handle();
            
            return Ok(result);
        }
  
    }
}