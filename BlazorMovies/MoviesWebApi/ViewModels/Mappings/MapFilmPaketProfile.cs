using AutoMapper;
using MoviesWebApi.Models;

namespace MoviesWebApi.ViewModels.Mappings
{
    public class MapFilmPaketProfile : Profile
    {
        public MapFilmPaketProfile()
        {
            CreateMap<FilmPaket, FilmPaketGetDto>();
        }
    }
}
