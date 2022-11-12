using AutoMapper;
using MoviesWebApi.Models;

namespace MoviesWebApi.ViewModels.Mappings
{
    public class MapPretplataProfile : Profile
    {
        public MapPretplataProfile()
        {
            CreateMap<Pretplata, PretplataGetDto>();
        }
    }
}
