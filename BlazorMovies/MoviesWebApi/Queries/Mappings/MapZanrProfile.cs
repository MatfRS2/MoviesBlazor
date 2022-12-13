using AutoMapper;

using MoviesWebApi.Models;
using System;

namespace MoviesWebApi.Queries.Mappings
{
    public class MapZanrProfile : Profile
    {
        public MapZanrProfile()
        {
            CreateMap<Zanr, MoviesWebApi.Queries.VratiZanrPoId.ZanrResponse>()
                .ConstructUsing(x => new MoviesWebApi.Queries.VratiZanrPoId.ZanrResponse (
                    x.ZanrId, x.Naziv));
            CreateMap<Zanr, MoviesWebApi.Queries.VratiZanrSve.ZanrResponse>()
                .ConstructUsing(x => new MoviesWebApi.Queries.VratiZanrSve.ZanrResponse(
                    x.ZanrId, x.Naziv));
        }
    }
}

