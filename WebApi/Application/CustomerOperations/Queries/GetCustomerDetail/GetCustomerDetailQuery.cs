using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.CustomerOperations.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQuery
    {
        public int CustomerId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCustomerDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public CustomerDetailViewModel Handle()
        {
            var customer = _dbContext.Customers.Include(x => x.CustomerFavoritGenres)
                                            .ThenInclude(cfg => cfg.Genre)
                                            .Include(x => x.Orders)
                                            .ThenInclude(x => x.Movie).SingleOrDefault(c => c.Id == CustomerId);

            if (customer is null)
                throw new InvalidOperationException("The user you are looking for was not found.");

            CustomerDetailViewModel returnObj = _mapper.Map<CustomerDetailViewModel>(customer);
            
            return returnObj;
        }
    }

    public class CustomerDetailViewModel
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public List<OrderVM> Orders { get; set; }
        public List<CustomerFavoritGenreVM> CustomerFavoritGenres { get; set; }

        public struct OrderVM
        {
            public int Id { get; set; }
            public MovieDetailVM Movie { get; set; }
            public decimal Price { get; set; }
            public DateTime PurchasedDate { get; set; }
            public bool InVisible { get; set; }
        }

        public struct CustomerFavoritGenreVM
        {
            public int Id { get; set; }
            public string Name { get; set; }

        }

        public struct MovieDetailVM
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public decimal Price { get; set; }
        }

    }
}