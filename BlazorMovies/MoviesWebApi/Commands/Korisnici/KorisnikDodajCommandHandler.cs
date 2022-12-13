using AutoMapper;
using MediatR;
using MoviesWebApi.Data;
using MoviesWebApi.Models;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Commands.Korisnici
{
    internal sealed class ZanrDodajCommandHandler : ICommandHandler<KorisnikDodajCommand>
    {

        private readonly MoviesWebApiContext _context;
        private readonly IMapper _mapper;

        public ZanrDodajCommandHandler(MoviesWebApiContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        async Task<Result> IRequestHandler<KorisnikDodajCommand, Result>.Handle(KorisnikDodajCommand request,
            CancellationToken cancellationToken)
        {
            var newObject = new Korisnik()
            {
                KorisnikId = request.KorisnikId,
                Email = request.EMail,
                Ime = request.Ime,
                Prezime = request.Prezime,   
                Potroseno= request.Potroseno,
            };
            _context.Korisnik.Add(newObject);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Sucess();
        }

    }
}
