using AutoMapper;
using MediatR;
using MoviesWebApi.Data;
using MoviesWebApi.Models;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Commands.Zanrovi
{
    internal sealed class ZanrDodajCommandHandler : ICommandHandler<ZanrDodajCommand>
    {

        private readonly MoviesWebApiContext _context;
        private readonly IMapper _mapper;

        public ZanrDodajCommandHandler(MoviesWebApiContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        async Task<Result> IRequestHandler<ZanrDodajCommand, Result>.Handle(ZanrDodajCommand request,
            CancellationToken cancellationToken)
        {
            var zanr = new Zanr()
            {
                ZanrId = request.ZanrId,
                Naziv = request.ZanrNaziv,
            };
            _context.Zanr.Add(zanr);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Sucess();
        }

    }
}
