using AutoMapper;
using MoviesWebApi.Models;

namespace MoviesWebApi.ViewModels.Mappings
{
    public class MapFilmProfile : Profile
    {
        public MapFilmProfile()
        {
            CreateMap<Film, FilmGetDto>();
            CreateMap<FilmPostDto, Film>();
            CreateMap<FilmPutDto, Film>();
        }
    }
}
