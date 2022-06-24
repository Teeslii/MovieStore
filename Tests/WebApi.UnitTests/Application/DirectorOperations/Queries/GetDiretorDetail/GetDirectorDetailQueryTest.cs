using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.UnitTests.TestSetup;
using WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorDetailQueryTests : IDisposable, IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetDirectorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.context;
            _mapper = testFixture.Mapper;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void WhenNotFoundDirectorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            int id = 999;

            GetDirectorDetailQuery query = new GetDirectorDetailQuery(_context,_mapper);
            query.DirectorId = id;

            FluentActions
                    .Invoking(() => query.Handle())
                    .Should().Throw<InvalidOperationException>()
                    .And.Message.Should().Be("The director you are looking for could not be found.");

        }

        [Fact]
        public void WhenValidDirectorIdIsGiven_Director_ShouldBeReturn()
        {
            var newDirector = new Director
            { 
                Name="Nadine", 
                Surname="Labaki"
            };
            _context.Directors.Add(newDirector);
            _context.SaveChanges();

            var Director = _context.Directors.SingleOrDefault(a => a.Name == newDirector.Name && a.Surname == newDirector.Surname);

            GetDirectorDetailQuery query = new GetDirectorDetailQuery(_context,_mapper);
            query.DirectorId = Director.Id;

            FluentActions.Invoking(()=>query.Handle()).Invoke();

            var findDirector = _context.Directors.SingleOrDefault(a => a.Id == Director.Id);
            
            findDirector.Should().NotBeNull();
            findDirector.Name.Should().Be(newDirector.Name);
            findDirector.Surname.Should().Be(newDirector.Surname);
          
        }

    }
}
