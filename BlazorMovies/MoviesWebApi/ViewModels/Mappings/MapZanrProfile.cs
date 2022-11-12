using AutoMapper;
using MoviesWebApi.Models;

namespace MoviesWebApi.ViewModels.Mappings
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
