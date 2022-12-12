using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviesWebApi.Data;
using MoviesWebApi.Models;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Queries.VratiZanrSve
{
    internal sealed class VratiZanrSveQueryHandler :
        IQueryHandler<VratiZanrSveQuery,List<ZanrResponse>>
    {
        private readonly MoviesWebApiContext _context;
        private readonly IMapper _mapper;

        public VratiZanrSveQueryHandler(MoviesWebApiContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<List<ZanrResponse>>> Handle(VratiZanrSveQuery request,
            CancellationToken cancellationToken)
        {
            List<Zanr> zanrovi = await _context.Zanr.ToListAsync(cancellationToken);
            List<ZanrResponse> ret = new List<ZanrResponse>();
            foreach(var z in zanrovi)
                ret.Add(new ZanrResponse(z.ZanrId, z.Naziv));
            return Result<List<ZanrResponse>>.Sucess(ret);
        }

    }
}
