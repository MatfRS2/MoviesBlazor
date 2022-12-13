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
            var foundObj = await _context.Korisnik.FindAsync(request.Id);
            if (foundObj == null)
                return Result.Faliure(new Error("Error.InvalidId", "Nije korektan identifikator za Korisnik."));
            foundObj.KorisnikId = request.KorisnikId;
            foundObj.Email = request.EMail;
            foundObj.Ime = request.Ime;
            foundObj.Prezime = request.Prezime;
            foundObj.Potroseno = request.Potroseno;
            _context.Entry(foundObj).State = EntityState.Modified;
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
