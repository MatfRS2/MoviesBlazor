using AutoMapper;
using BlazorMoviesApp.Models;

namespace BlazorMoviesApp.ViewModels
{
    public class MapFilmProfile : Profile
    {
        public MapFilmProfile()
        {
            CreateMap<Film, FilmGetDto>();
            CreateMap<FilmAddDto, Film>();
            CreateMap<FilmUpdateDto, Film>();
        }
    }
}
