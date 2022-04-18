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
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieOfActors>(ConfigureMovieOfActor);
           

            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureMovieOfActor(EntityTypeBuilder<MovieOfActors> modelBuilder)
        {
            //modelBuilder.ToTable("MovieActor");
            modelBuilder.HasKey(mc => new { mc.MovieId, mc.ActorId });
            modelBuilder.HasOne(mc => mc.Movie).WithMany(g => g.MovieOfActors).HasForeignKey(mg => mg.MovieId);
            modelBuilder.HasOne(mc => mc.Actor).WithMany(g => g.MovieOfActors).HasForeignKey(mg => mg.ActorId);
        }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}