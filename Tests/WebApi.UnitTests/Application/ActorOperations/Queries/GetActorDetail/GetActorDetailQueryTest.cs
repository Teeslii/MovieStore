using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.ActorOperations.Queries.GetActorDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.ActorOperations.Queries.GetActorDetail
{
    public class GetActorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetActorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotFoundActorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            int id = -1;

            GetActorDetailQuery query = new GetActorDetailQuery(_context,_mapper);
            query.ActorId = id;

            FluentActions
                    .Invoking(()=>query.Handle())
                    .Should().Throw<InvalidOperationException>()
                    .And.Message.Should().Be("The actor you were looking for was not found.");
        }

        [Fact]
        public void WhenValidActorIdIsGiven_Actor_ShouldBeReturn()
        {
            var newActor = new Actor {Name="Charlene", Surname="Swankie"};
            _context.Actors.Add(newActor);
            _context.SaveChanges();

            var actor = _context.Actors.SingleOrDefault(a => a.Name == newActor.Name && a.Surname == newActor.Surname);

            GetActorDetailQuery query = new GetActorDetailQuery(_context,_mapper);
            query.ActorId = actor.Id;

            FluentActions
                .Invoking(()=>query.Handle()).Invoke();

            var findActor = _context.Actors.SingleOrDefault(a => a.Id == actor.Id);
            
            findActor.Should().NotBeNull();
            findActor.Name.Should().Be(newActor.Name);
            findActor.Surname.Should().Be(newActor.Surname);
        }

    }
}
