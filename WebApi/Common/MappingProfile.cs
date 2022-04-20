


using AutoMapper;
using WebApi.Entities;

using WebApi.Application.MovieOperations.Queries.GetMovies;
using static WebApi.Application.MovieOperations.Queries.GetMovieDetail.MovieDetailViewModel;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;

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
        }  
       
    }

}
