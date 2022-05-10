using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Directors
    {
        public static void AddDirectors(this MovieStoreDbContext context)
        {
            context.Directors.AddRange(
                     new Director
                     {
                          Name = "Leigh",
                          Surname = "Whannell"
                     },
                     new Director
                     {
                          Name = "William Brent",
                          Surname = "Bell"
                     },
                      new Director
                     {
                          Name = "Hans",
                          Surname = "Weingartner"
                     },
                      new Director
                     {
                          Name = "Chloé",
                          Surname = "Zhao"
                     },
                       new Director
                     {
                          Name = "Pete",
                          Surname = "Docter"
                     }
                );
        }
    }
}