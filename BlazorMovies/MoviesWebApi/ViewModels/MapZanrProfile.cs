using AutoMapper;
using MoviesWebApi.Models;

namespace MoviesWebApi.ViewModels
{
    public class MapZanrProfile : Profile
    {
        public MapZanrProfile()
        {
            CreateMap<Zanr, ZanrDto>();
            CreateMap<ZanrDto, Zanr>();
        }
    }
}
