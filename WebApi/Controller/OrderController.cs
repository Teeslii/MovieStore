using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.OrderOperations.Commands.CreateOrder;
using WebApi.Application.OrderOperations.Commands.UpdateOrder;
using WebApi.Application.OrderOperations.Queries.GetOrderByCustomerIdDetail;
using WebApi.Application.OrderOperations.Queries.GetOrderDetail;
using WebApi.DBOperations;

namespace WebApi.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
         private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public OrderController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOrderByCutomerId(int customerId)
        {
            GetOrderByCustomerIdQuery query = new GetOrderByCustomerIdQuery(_context, _mapper);
            query.CustomerId = customerId;

            GetOrderByCustomerIdQueryValidator validator = new GetOrderByCustomerIdQueryValidator();
            validator.ValidateAndThrow(query);

            var result = query.Handle();

            return Ok(result);
        }

      
        [HttpGet("{id}")]

        public IActionResult GetOrderDetail(int id)
        {
            GetOrderDetailQuery query = new GetOrderDetailQuery(_context, _mapper);
            query.OrderId = id;

            GetOrderDetailQueryValidator validator = new GetOrderDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var result = query.Handle();

            return Ok(result);
        }

      
        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderModel model)
        {
            CreateOrderCommand command = new CreateOrderCommand(_context, _mapper);
            command.Model = model;

            CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
            validator.ValidateAndThrow(command);

           command.Handle();
           
           return Ok();
           
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrderMovie(int id, [FromBody] UpdateOrderModel model)
        {
            UpdateOrderCommand command = new UpdateOrderCommand(_context);
            command.OrderId = id;
            command.Model = model;

            UpdateOrderCommandValidator validator = new UpdateOrderCommandValidator();
            validator.ValidateAndThrow(command);

           command.Handle();
          
           return Ok();
        }    
    }
}        