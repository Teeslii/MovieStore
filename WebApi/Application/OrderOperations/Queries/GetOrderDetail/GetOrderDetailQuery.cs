using AutoMapper;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.OrderOperations.Queries.GetOrderDetail
{
    public class GetOrderDetailQuery
    {
        public int OrderId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetOrderDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public OrderDetailViewModel Handle(){
            var orderMovie = _dbContext.Orders.Include(x => x.Customer).Include(x => x.Movie).SingleOrDefault(x=>x.Id == OrderId && x.InVisible == true);

            if(orderMovie is null)
                throw new InvalidOperationException("Purchase not found.");

            OrderDetailViewModel returnObj = _mapper.Map<OrderDetailViewModel>(orderMovie);

            return returnObj;
        }
    }

    public class OrderDetailViewModel
    {
        public string Customer { get; set; }
        public string Movie { get; set; }
        public DateTime PurchasedDate { get; set; }
        public decimal Price { get; set; }
    }
}