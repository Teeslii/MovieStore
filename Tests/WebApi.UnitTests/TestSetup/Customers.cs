using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Customers
    {
        public static void AddCustomers(this MovieStoreDbContext context)
        {
            context.Customers.AddRange(
                      new Customer{
                           Name = "Selin",
                           Surname = "Şans",
                           Email = "selinsans@outlook.com",
                           Password = "Selin0123"
                      },
                      new Customer{
                           Name = "Eylül",
                           Surname = "Bugün",
                           Email = "eylulbugun@gmail.com",
                           Password = "Eylül0123"
                           
                      }
            );          
        }
    }
}