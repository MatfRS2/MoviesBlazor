using AutoMapper;
using MediatR;
using MoviesWebApi.Data;
using MoviesWebApi.Models;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Commands.Korisnici
{
    internal sealed class KorisnikObrisiCommandHandler : ICommandHandler<KorisnikObrisiCommand>
    {

        private readonly MoviesWebApiContext _context;
        private readonly IMapper _mapper;

        public KorisnikObrisiCommandHandler(MoviesWebApiContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        async Task<Result> IRequestHandler<KorisnikObrisiCommand, Result>.Handle(KorisnikObrisiCommand request,
            CancellationToken cancellationToken)
        {
            var foundObj = await _context.Korisnik.FindAsync(request.Id);
            if (foundObj == null)
            {
                return Result.Faliure(new Error("Error.NoData", "Nema korisnika sa datim identifikatorom."));
            }
            _context.Korisnik.Remove(foundObj);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Sucess();
        }

    }
}
