using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviesWebApi.Data;
using MoviesWebApi.Models;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Queries.VratiKorisnikSve
{
    internal sealed class VratiKorisnikSveQueryHandler :
        IQueryHandler<VratiKorisnikSveQuery,List<KorisnikResponse>>
    {
        private readonly MoviesWebApiContext _context;
        private readonly IMapper _mapper;

        public VratiKorisnikSveQueryHandler(MoviesWebApiContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<List<KorisnikResponse>>> Handle(VratiKorisnikSveQuery request,
            CancellationToken cancellationToken)
        {
            List<Korisnik> resultList = await _context.Korisnik.ToListAsync(cancellationToken);
            List<KorisnikResponse> ret = new List<KorisnikResponse>();
            foreach(var r in resultList)
                ret.Add(new KorisnikResponse(r.KorisnikId, r.Email, r.Ime, r.Prezime, r.Potroseno));
            return Result<List<KorisnikResponse>>.Sucess(ret);
        }

    }
}
