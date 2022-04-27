using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.ActorOperations.Command.CreateActor;
using WebApi.Application.ActorOperations.Queries.GetActorDetail;
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
        
        
        [HttpGet("{id}")]      
        public IActionResult GetActorDetail(int id)
        {
            GetActorDetailQuery query = new GetActorDetailQuery(_context, _mapper);
            query.ActorId = id;

            GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
            validator.ValidateAndThrow(query);
            
            var result = query.Handle();
            
            return Ok(result);
        }
        
        [HttpPost]
        public IActionResult CreateActor([FromBody] CreateActorViewModel model)
        {
            CreateActorCommand command = new CreateActorCommand(_context, _mapper);
            command.Model = model;

            CreateActorCommandValidator validator = new CreateActorCommandValidator();
            validator.ValidateAndThrow(command);
            
            command.Handle();
            
            return Ok();
        }
    }
}