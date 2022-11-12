using AutoMapper;
using MoviesWebApi.Models;

namespace MoviesWebApi.ViewModels.Mappings
{
    public class MapPaketProfile : Profile
    {
        public MapPaketProfile()
        {
            CreateMap<Paket, PaketGetDto>();
        }
    }
}
