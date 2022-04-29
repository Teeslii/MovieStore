


using AutoMapper;
using WebApi.Entities;

using WebApi.Application.MovieOperations.Queries.GetMovies;
using static WebApi.Application.MovieOperations.Queries.GetMovieDetail.MovieDetailViewModel;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using WebApi.Application.MovieOperations.Command.CreateMovie;
using WebApi.Application.ActorOperations.Queries.GetActors;
using WebApi.Application.ActorOperations.Queries.GetActorDetail;
using WebApi.Application.ActorOperations.Command.CreateActor;
using WebApi.Application.MovieOfActorOperations.Queries.GetMovieActors;
using static WebApi.Application.ActorOperations.Queries.GetActors.ActorsViewModel;
using WebApi.Application.MovieOfActorOperations.Queries.GetActorMovies;
using WebApi.Application.MovieActorOperations.Commands.CreateMovieActor;
using WebApi.Application.DirectorOperations.Queries.GetDirectors;
using WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using WebApi.Application.DirectorOperations.Commands.CreateDirector;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Commands.CreateGenre;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Movie, MoviesViewModel>().ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.MovieOfActors.Select(ma => ma.Actor).ToList()))
                                                    .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name.ToString()))
                                                    .ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director.NameSurname.ToString()));
            CreateMap<Actor, MoviesViewModel.MovieOfActorsViewModel>();
            CreateMap<Movie, MovieDetailViewModel>().ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.MovieOfActors.Select(ma => ma.Actor).ToList()))
                                                    .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name.ToString()))
                                                    .ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director.NameSurname.ToString()));
            CreateMap<Actor, MovieOfActorsDetailQueryViewModel>();
    
            CreateMap<CreateMovieViewModel, Movie>();

            CreateMap<Actor, ActorsViewModel>().ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.MovieOfActors.Select(ma => ma.Movie).ToList()));
            CreateMap<Movie, ActorsViewModel.ActorMoviesViewModel>();

            CreateMap<Actor, ActorDetailViewModel>().ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.MovieOfActors.Select(ma => ma.Movie).ToList()));
            CreateMap<Movie, ActorDetailViewModel.ActorMoviesDetailViewModel>();

            CreateMap<CreateActorViewModel, Actor>();

            CreateMap<MovieOfActors, MovieOfActorsViewModel>().ForMember(dest => dest.NameSurname, opt => opt.MapFrom(src => src.Actor.NameSurname))
                                                    .ForMember(dest => dest.ActorId, opt => opt.MapFrom(src => src.Actor.Id));

            
            CreateMap<MovieOfActors, ActorOfMoviesViewModel>().ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Movie.Director.NameSurname))
                                                    .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Movie.Genre.Name))
                                                    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Movie.Title))
                                                    .ForMember(dest => dest.ReleaseYear, opt => opt.MapFrom(src => src.Movie.ReleaseYear));

            CreateMap<CreateMovieActorViewModel, MovieOfActors>();


            CreateMap<Director, DirectorsViewModel>().ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies.ToList()));
            CreateMap<Movie, DirectorsViewModel.DirectorMoviesVM>();

            CreateMap<Director, DirectorDetailViewModel>().ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies.ToList()));
            CreateMap<Movie, DirectorDetailViewModel.DirectorMoviesViewModel>();

            CreateMap<CreateDirectorViewModel, Director>();

            CreateMap<Genre, GenresViewModel>();
          
            CreateMap<Genre, GenreDetailViewModel>();
         
            CreateMap<CreateGenreViewModel, Genre>();
        }  
    }

}
