using AutoMapper;

using MoviesWebApi.Models;
using System;

namespace MoviesWebApi.Queries.Mappings
{
    public class MapKorisnikProfile : Profile
    {
        public MapKorisnikProfile()
        {
            CreateMap<Korisnik, MoviesWebApi.Queries.VratiKorisnikPoId.KorisnikResponse>()
                .ConstructUsing(x => new MoviesWebApi.Queries.VratiKorisnikPoId.KorisnikResponse(
                    x.KorisnikId,x.Email, x.Ime, x.Prezime, x.Potroseno)); 
            CreateMap<Korisnik, MoviesWebApi.Queries.VratiKorisnikSve.KorisnikResponse>()
                .ConstructUsing(x => new MoviesWebApi.Queries.VratiKorisnikSve.KorisnikResponse(
                    x.KorisnikId, x.Email, x.Ime, x.Prezime, x.Potroseno));
        }
    }
}
