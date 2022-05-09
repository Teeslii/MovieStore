 
 using System;
 using Microsoft.EntityFrameworkCore;
 using System.Linq;
 using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
   public class DataGenerator
   {
        public static void Initialize(IServiceProvider serviceProvider)
        {

         using(var context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
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
                if(context.Movies.Any())
                {
                     return;
                }
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
                context.Actors.AddRange(
                     new Actor
                     {
                          Name = "Elisabeth",
                          Surname = "Moss"
                          
                     },
                      new Actor
                     {
                          Name = "Madeline",   
                          Surname = "Brewer"
                     },
                      new Actor
                     {
                          Name = "Mala",
                          Surname = "Emde"
                     },
                     new Actor
                     {
                           Name = "Frances",
                           Surname = "McDormand"
                     },
                      new Actor
                     {
                          Name = "Jamie",
                          Surname = "Foxx"
                     },
                        new Actor
                     {
                          Name = "Anton",
                          Surname = "Spieker"
                     },
                     new Actor
                     {
                        Name = "Linda",
                        Surname = "May"
                     }
                );
         
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
              
                if (context.MovieOfActors.Any())
                {
                    return;
                }
               context.MovieOfActors.AddRange(MovieOfActors);                 
              
               List<CustomerFavoritGenre> customerFavoritGenres = new List<CustomerFavoritGenre>
                {
                    new CustomerFavoritGenre{ CustomerId = 1, GenreId = 1 },
                    new CustomerFavoritGenre{ CustomerId = 1, GenreId = 2 },
                    new CustomerFavoritGenre{ CustomerId = 1, GenreId = 3 },
                    new CustomerFavoritGenre{ CustomerId = 2, GenreId = 1 },
                    new CustomerFavoritGenre{ CustomerId = 2, GenreId = 3 },
                };

                
               context.CustomerFavoritGenres.AddRange(customerFavoritGenres);

                if(context.CustomerFavoritGenres.Any())
                {
                     return;
                }
                
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
                context.SaveChanges();
           }
    }
   }
}