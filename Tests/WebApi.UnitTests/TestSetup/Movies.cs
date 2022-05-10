using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Movies
    {
        public static void AddMovies(this MovieStoreDbContext context)
        {
            context.Movies.AddRange(
                     new Movie 
                     {
                          Title = "The Invisible Man",
                          GenreId = 1 ,
                          DirectorId = 1 ,
                          ReleaseYear = 2020,   
                          Price = 70000 
                     },
                     new Movie
                     {
                          Title = "Separation",
                          GenreId = 1,
                          DirectorId = 2,
                          ReleaseYear = 2021,
                          Price = 45000
                     },
                     new Movie 
                     {
                          Title = "303",
                          GenreId = 2,
                          DirectorId = 3,
                          ReleaseYear = 2018,
                          Price = 19000
                     },
                     new Movie
                     {
                          Title = "Nomadland",
                          GenreId = 3,
                          DirectorId = 4,
                          ReleaseYear = 2021,
                          Price = 5000
                     },
                      new Movie
                     {
                          Title = "Soul",
                          GenreId = 4,
                          DirectorId = 5,
                          Price = 7000,
                          ReleaseYear = 2020
                     }
                );
        }
    }
}