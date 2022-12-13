using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviesWebApi.Data;
using MoviesWebApi.Models;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Commands.Korisnici
{
    internal sealed class KorisnikPostaviCommandHandler : ICommandHandler<KorisnikPostaviCommand>
    {

        private readonly MoviesWebApiContext _context;
        private readonly IMapper _mapper;

        public KorisnikPostaviCommandHandler(MoviesWebApiContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        async Task<Result> IRequestHandler<KorisnikPostaviCommand, Result>.Handle(KorisnikPostaviCommand request,
            CancellationToken cancellationToken)
        {
            var resultOne = await _context.Korisnik.FindAsync(request.Id);
            if (resultOne == null)
                return Result.Faliure(new Error("Error.InvalidId", "Nije korektan identifikator za Korisnik."));
            resultOne.KorisnikId = request.KorisnikId;
            resultOne.Email = request.EMail;
            resultOne.Ime = request.Ime;
            resultOne.Prezime = request.Prezime;
            resultOne.Potroseno = request.Potorseno;
            _context.Entry(resultOne).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KorisnikExists(request.Id))
                {
                    return Result.Faliure(new Error("Error.ConcurencyPhantom",
                        "Greška zbog fantomskih podataka pri konkurentnom ažuriranju."));
                }
                else
                {
                    Result.Faliure(new Error("Error.ConcurencyUpdate",
                        "Greška pri konkurentnom ažuriranju."));
                }
            }
            return Result.Sucess();

        }

        private bool KorisnikExists(int id)
        {
            return _context.Korisnik.Any(e => e.KorisnikId == id);
        }
    }
}
