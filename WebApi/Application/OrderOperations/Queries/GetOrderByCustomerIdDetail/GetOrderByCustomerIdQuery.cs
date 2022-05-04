using AutoMapper;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApi.DBOperations;

namespace WebApi.Application.OrderOperations.Queries.GetOrderByCustomerIdDetail
{
    public class GetOrderByCustomerIdQuery
    {
        public int CustomerId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetOrderByCustomerIdQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<OrderByCustomerIdViewModel> Handle()
        {
            var orderMovie = _dbContext.Orders.Include(x => x.Movie).Where(x => x.CustomerId == CustomerId);

            if (orderMovie is null)
                throw new InvalidOperationException("No purchases found.");

            List<OrderByCustomerIdViewModel> returnObj = _mapper.Map<List<OrderByCustomerIdViewModel>>(orderMovie);

            return returnObj;
        }
    }

    public class OrderByCustomerIdViewModel
    {
        public int Id { get; set; }
        public string Movie { get; set; }
        public DateTime PurchasedDate { get; set; }
        public decimal Price { get; set; }
        public bool InVisible { get; set; }
    }
}