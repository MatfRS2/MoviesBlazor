using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviesWebApi.Data;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Queries.VratiKorisnikPoId
{
    internal sealed class VratiKorisnikPoIdQueryHandler :
        IQueryHandler<VratiKorisnikPoIdQuery, KorisnikResponse>
    {
        private readonly MoviesWebApiContext _context;
        private readonly IMapper _mapper;

        public VratiKorisnikPoIdQueryHandler(MoviesWebApiContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<KorisnikResponse>> Handle(VratiKorisnikPoIdQuery request, 
            CancellationToken cancellationToken)
        {
            var resultSingle = await _context.Korisnik.Where(row => row.KorisnikId == request.KorisnikId).SingleOrDefaultAsync();
            if (resultSingle is null)
            {
                return Result<KorisnikResponse>.Faliure(new Error("Error.NoData", 
                    $"Nema korisnika sa identifikatorom {request.KorisnikId}."));
            }
            return _mapper.Map<KorisnikResponse>(resultSingle); 
        }
    }
}
