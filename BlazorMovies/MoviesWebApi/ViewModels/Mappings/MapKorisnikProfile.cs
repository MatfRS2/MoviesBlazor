using AutoMapper;

using MoviesWebApi.Models;

namespace MoviesWebApi.ViewModels.Mappings
{
    public class MapKorisnikProfile : Profile
    {
        public MapKorisnikProfile()
        {
            CreateMap<Korisnik, KorisnikGetDto>();
        }
    }
}
