using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public interface IMovieStoreDbContext
    {
        DbSet<Actor> Actors { get; set;}
        DbSet<Genre> Genres { get; set;}
        DbSet<Director> Directors { get; set;}
        DbSet<Movie> Movies { get; set;}
        DbSet<MovieOfActors> MovieOfActors  { get; set;}
        DbSet<Order> Orders { get; set;}
        DbSet<Customer> Customers { get; set; }
        DbSet<CustomerFavoritGenre> CustomerFavoritGenres { get; set; }

        int SaveChanges();
    }
}