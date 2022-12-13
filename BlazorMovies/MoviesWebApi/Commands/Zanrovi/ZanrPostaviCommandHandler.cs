using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviesWebApi.Data;
using MoviesWebApi.Models;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Commands.Zanrovi
{
    internal sealed class ZanrPostaviCommandHandler : ICommandHandler<ZanrPostaviCommand>
    {

        private readonly MoviesWebApiContext _context;
        private readonly IMapper _mapper;

        public ZanrPostaviCommandHandler(MoviesWebApiContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        async Task<Result> IRequestHandler<ZanrPostaviCommand, Result>.Handle(ZanrPostaviCommand request,
            CancellationToken cancellationToken)
        {
            var foundObj = await _context.Zanr.FindAsync(request.Id);
            if (foundObj == null)
                return Result.Faliure(new Error("Error.InvalidId", "Nije korektan identifikator za Zanr."));
            foundObj.ZanrId = request.ZanrId;
            foundObj.Naziv = request.ZanrNaziv;
            _context.Entry(foundObj).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZanrExists(request.Id))
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

        private bool ZanrExists(int id)
        {
            return _context.Zanr.Any(e => e.ZanrId == id);
        }
    }
}
