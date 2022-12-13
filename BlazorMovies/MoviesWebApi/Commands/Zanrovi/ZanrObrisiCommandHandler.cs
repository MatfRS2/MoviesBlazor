using AutoMapper;
using MediatR;
using MoviesWebApi.Data;
using MoviesWebApi.Models;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Commands.Zanrovi
{
    internal sealed class ZanrObrisiCommandHandler : ICommandHandler<ZanrObrisiCommand>
    {

        private readonly MoviesWebApiContext _context;
        private readonly IMapper _mapper;

        public ZanrObrisiCommandHandler(MoviesWebApiContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        async Task<Result> IRequestHandler<ZanrObrisiCommand, Result>.Handle(ZanrObrisiCommand request,
            CancellationToken cancellationToken)
        {
            var foundObj = await _context.Zanr.FindAsync(request.Id);
            if (foundObj == null)
            {
                return Result.Faliure(new Error("Error.NoData", "Nema zanra sa datim identifikatorom."));
            }
            _context.Zanr.Remove(foundObj);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Sucess();
        }

    }
}
