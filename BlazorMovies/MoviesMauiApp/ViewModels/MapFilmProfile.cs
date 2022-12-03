using AutoMapper;
using MoviesMauiApp.Models;

namespace MoviesMauiApp.ViewModels
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
