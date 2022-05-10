using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class MovieActors
    {
        public static void AddMovieActors(this MovieStoreDbContext context)
        {
             List<MovieOfActors> MovieOfActors = new List<MovieOfActors>
                {
                     new MovieOfActors {
                         MovieId = 1,
                         ActorId = 1
                     },
                      new MovieOfActors {
                         MovieId = 2,
                         ActorId = 2
                     },
                      new MovieOfActors {
                         MovieId = 3,
                         ActorId = 3
                     },
                     new MovieOfActors {
                         MovieId = 4,
                         ActorId = 4
                     },
                     new MovieOfActors {
                         MovieId = 5,
                         ActorId = 5
                     },
                      new MovieOfActors {
                         MovieId = 3,
                         ActorId = 6
                     }
              };  
            
            context.MovieOfActors.AddRange(MovieOfActors); 
        }
    }
}