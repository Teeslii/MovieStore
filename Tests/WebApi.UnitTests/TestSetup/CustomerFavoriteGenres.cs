using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class CustomerFavoriteGenres
    {
         public static void AddCustomerFavoriteGenres(this MovieStoreDbContext context)
         {
           List<CustomerFavoriteGenre> customerFavoriteGenres = new List<CustomerFavoriteGenre>
                {
                    new CustomerFavoriteGenre{ CustomerId = 1, GenreId = 1 },
                    new CustomerFavoriteGenre{ CustomerId = 1, GenreId = 2 },
                    new CustomerFavoriteGenre{ CustomerId = 1, GenreId = 3 },
                    new CustomerFavoriteGenre{ CustomerId = 2, GenreId = 1 },
                    new CustomerFavoriteGenre{ CustomerId = 2, GenreId = 3 },
                };

                
               context.CustomerFavoriteGenre.AddRange(customerFavoriteGenres);
         }
    }
}