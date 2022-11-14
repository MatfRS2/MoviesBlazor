using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviesWebApi.Data;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Queries.VratiZanrPoId
{
    internal sealed class VratiSveZanroveQueryHandler :
        IQueryHandler<VratiZanrPoIdQuery, ZanrResponse>
    {
        private readonly MoviesWebApiContext _context;
        private readonly IMapper _mapper;

        public VratiSveZanroveQueryHandler(MoviesWebApiContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<ZanrResponse>> Handle(VratiZanrPoIdQuery request, 
            CancellationToken cancellationToken)
        {
            var zanr = await _context.Zanr.Where(z => z.ZanrId == request.ZanrId).SingleOrDefaultAsync();
            if (zanr is null)
            {
                return Result<ZanrResponse>.Faliure(new Error("Error.NoData", 
                    $"Nema zanra sa identifikatorom {request.ZanrId}."));
            }
            var resp = new ZanrResponse(request.ZanrId, zanr.Naziv);
            return resp;
        }
    }
}
