using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public interface IMovieStoreDbContext
    {
        public DbSet<Actor> Actors { get; set;}
        public DbSet<Genre> Genres { get; set;}
        public DbSet<Director> Directors { get; set;}
        public DbSet<Movie> Movies { get; set;}
        public DbSet<Order> Orders { get; set;}

        int SaveChanges();
    }
}