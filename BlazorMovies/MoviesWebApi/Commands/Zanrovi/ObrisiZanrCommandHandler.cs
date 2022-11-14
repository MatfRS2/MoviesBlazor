using AutoMapper;
using MediatR;
using MoviesWebApi.Data;
using MoviesWebApi.Models;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Commands.Zanrovi
{
    internal sealed class ObrisiZanrCommandHandler : ICommandHandler<ObrisiZanrCommand>
    {

        private readonly MoviesWebApiContext _context;
        private readonly IMapper _mapper;

        public ObrisiZanrCommandHandler(MoviesWebApiContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        async Task<Result> IRequestHandler<ObrisiZanrCommand, Result>.Handle(ObrisiZanrCommand request,
            CancellationToken cancellationToken)
        {
            var zanr = await _context.Zanr.FindAsync(request.Id);
            if (zanr == null)
            {
                return Result.Faliure(new Error("Error.NoData", "Nema zanra sa datim identifikatorom."));
            }
            _context.Zanr.Remove(zanr);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Sucess();
        }

    }
}
