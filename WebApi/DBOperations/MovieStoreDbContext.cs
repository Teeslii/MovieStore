using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class MovieStoreDbContext : DbContext, IMovieStoreDbContext
    {
        public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options): base (options) {}
        public DbSet<Actor> Actors { get; set;}
        public DbSet<Genre> Genres { get; set;}
        public DbSet<Director> Directors { get; set;}
        public DbSet<Movie> Movies { get; set;}
        public DbSet<MovieOfActors> MovieOfActors  { get; set;}
        public DbSet<Order> Orders { get; set;}
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerFavoritGenre> CustomerFavoritGenres { get; set; }
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieOfActors>(ConfigureMovieOfActor);
            modelBuilder.Entity<CustomerFavoritGenre>(ConfigureCustomerFavoritGenre);

            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureMovieOfActor(EntityTypeBuilder<MovieOfActors> modelBuilder)
        {
            //modelBuilder.ToTable("MovieActor");
            modelBuilder.HasKey(mc => new { mc.MovieId, mc.ActorId });
            modelBuilder.HasOne(mc => mc.Movie).WithMany(g => g.MovieOfActors).HasForeignKey(mg => mg.MovieId);
            modelBuilder.HasOne(mc => mc.Actor).WithMany(g => g.MovieOfActors).HasForeignKey(mg => mg.ActorId);
        }
        private void ConfigureCustomerFavoritGenre(EntityTypeBuilder<CustomerFavoritGenre> modelBuilder)
        {
            modelBuilder.HasKey(sc => new { sc.GenreId, sc.CustomerId });
            modelBuilder.HasOne<Customer>(x => x.Customer).WithMany(a => a.CustomerFavoritGenres).HasForeignKey(x => x.CustomerId);
            modelBuilder.HasOne<Genre>(x => x.Genre).WithMany(m => m.CustomerFavoritGenres).HasForeignKey(x => x.GenreId);
        } 
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}