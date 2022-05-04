using AutoMapper;
using WebApi.Entities;
using System;
using WebApi.DBOperations;

namespace WebApi.Application.OrderOperations.Commands.CreateOrder
{
    public class CreateOrderCommand
    {
        public CreateOrderModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateOrderCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var orderMovie = _mapper.Map<Order>(Model);

            orderMovie.PurchasedDate = DateTime.Now;

            _dbContext.Orders.Add(orderMovie);

            _dbContext.SaveChanges();
        }
    }

    public class CreateOrderModel
    {
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public decimal Price { get; set; }
    }
}