using AutoMapper;
using WebApi.Entities;
using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.OrderOperations.Commands.UpdateOrder
{
    public class UpdateOrderCommand
    {
        public int OrderId { get; set; }
        public UpdateOrderModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateOrderCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var orderMovie = _dbContext.Orders.SingleOrDefault(om => om.Id == OrderId);
            
            if(orderMovie is null)
                      throw new InvalidOperationException("There is no order that can be updated.");   
            orderMovie.InVisible = Model.InVisible != default ? orderMovie.InVisible : Model.InVisible;

            _dbContext.SaveChanges();
        }
    }

    public class UpdateOrderModel
    {
        public bool InVisible { get; set; }
    }
}