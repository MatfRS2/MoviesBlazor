using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviesWebApi.Data;
using MoviesWebApi.Models;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Commands.Zanrovi
{
    internal sealed class PostaviZanrCommandHandler : ICommandHandler<PostaviZanrCommand>
    {

        private readonly MoviesWebApiContext _context;
        private readonly IMapper _mapper;

        public PostaviZanrCommandHandler(MoviesWebApiContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        async Task<Result> IRequestHandler<PostaviZanrCommand, Result>.Handle(PostaviZanrCommand request,
            CancellationToken cancellationToken)
        {
            var zanr = await _context.Zanr.FindAsync(request.Id);
            if (zanr == null)
                return Result.Faliure(new Error("Error.InvalidId", "Nije korektan identifikator za Zanr."));
            zanr.ZanrId = request.ZanrId;
            zanr.Naziv = request.ZanrNaziv;
            _context.Entry(zanr).State = EntityState.Modified;
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
