using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Orders
    {
        public static void AddOrders(this MovieStoreDbContext context)
        {
                context.Orders.AddRange(
                    new Order{
                         MovieId = 3,
                         CustomerId = 1,
                         PurchasedDate = new DateTime(2022,03,1),
                         Price = 40
                    },
                     new Order{
                         MovieId = 4,
                         CustomerId = 1,
                         PurchasedDate = new DateTime(2022,03,1),
                         Price = 80
                    },
                     new Order{
                         MovieId = 2,
                         CustomerId = 2,
                         PurchasedDate = new DateTime(2022,02,23),
                         Price = 50
                    }

                );
        }
    }
}