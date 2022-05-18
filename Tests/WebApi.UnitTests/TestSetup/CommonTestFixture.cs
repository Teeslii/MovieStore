using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.UnitTests.TestSetup
{
    public class CommonTestFixture
    {
        public MovieStoreDbContext context { get; set; }
        public IMapper Mapper { get; set; }
        public IConfiguration Configuration { get; set;}
        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<MovieStoreDbContext>().UseInMemoryDatabase(databaseName: "MovieStoreTestDB").Options;
            context = new MovieStoreDbContext(options);
            context.Database.EnsureCreated();

            context.AddMovies();
            context.AddGenres();
            context.AddActors();
            context.AddDirectors();
            context.AddMovieActors();
            context.AddOrders();
            context.AddCustomers();
            context.AddCustomerFavoriteGenres();

            context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
            
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        }    
    }
}