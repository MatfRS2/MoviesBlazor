using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviesWebApi.Data;
using MoviesWebApi.Models;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Queries.VratiSveZanrove
{
    internal sealed class VratiSveZanroveQueryHandler :
        IQueryHandler<VratiSveZanroveQuery,List<ZanrJedanResponse>>
    {
        private readonly MoviesWebApiContext _context;
        private readonly IMapper _mapper;

        public VratiSveZanroveQueryHandler(MoviesWebApiContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<List<ZanrJedanResponse>>> Handle(VratiSveZanroveQuery request,
            CancellationToken cancellationToken)
        {
            List<Zanr> zanrovi = await _context.Zanr.ToListAsync(cancellationToken);
            List<ZanrJedanResponse> ret = new List<ZanrJedanResponse>();
            foreach(var z in zanrovi)
                ret.Add(new ZanrJedanResponse(z.ZanrId, z.Naziv));
            return Result<List<ZanrJedanResponse>>.Sucess(ret);
        }

    }
}
