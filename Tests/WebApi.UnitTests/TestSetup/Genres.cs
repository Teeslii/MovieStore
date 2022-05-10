using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Genres
    {
         public static void AddGenres(this MovieStoreDbContext context)
         {
              context.Genres.AddRange(
                    new Genre
                    {
                         Name = "Income"
                    },
                     new Genre
                    {
                         Name = "Romantic"
                    },
                    new Genre
                    {
                         Name = "Drama"
                    },
                     new Genre
                    {
                         Name = "Comedy"
                    }
               );
         }
    }
}