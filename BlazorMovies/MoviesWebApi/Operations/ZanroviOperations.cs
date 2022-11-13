using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviesWebApi.Data;
using MoviesWebApi.Models;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Operations
{
    public class ZanroviOperations: IZanroviOperations
    {
        private readonly MoviesWebApiContext _context;
        private readonly IMapper _mapper;

        public ZanroviOperations(MoviesWebApiContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<List<ZanrDto>>> SviZanrovi()
        {
            List<Zanr> zanrovi = await _context.Zanr.ToListAsync();
            List<ZanrDto> ret = _mapper.Map<List<ZanrDto>>(zanrovi);
            return ret;
        }

        public async Task<Result<ZanrDto>> ZanrPoId(int id)
        {
            var zanr = await _context.Zanr.Where(z => z.ZanrId == id).SingleOrDefaultAsync();
            if (zanr == null)
            {
                return Result<ZanrDto>.Faliure(new Error("Error.NoData", "Nema zanra sa datim identifikatorom." ));
            }
            return _mapper.Map<ZanrDto>(zanr);
        }


        public async Task<Result> PostaviZanr(int id, ZanrDto zanrDto)
        {
            var zanr = await _context.Zanr.FindAsync(id);
            if (zanr == null)
                return Result.Faliure( new Error( "Error.InvalidId", "Nije korektan identifikator za Zanr." ) );
            _mapper.Map<ZanrDto, Zanr>(zanrDto, zanr);
            _context.Entry(zanr).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZanrExists(id))
                {
                    return Result.Faliure( new Error("Error.ConcurencyPhantom", 
                        "Greška zbog fantomskih podataka pri konkurentnom ažuriranju.") );
                }
                else
                {
                    Result.Faliure(new Error("Error.ConcurencyUpdate",
                        "Greška pri konkurentnom ažuriranju."));
                }
            }
            return Result.Sucess();
        }

        public async Task<Result> DodajZanr(ZanrDto zanrDto)
        {
            Zanr zanr = _mapper.Map<Zanr>(zanrDto);
            _context.Zanr.Add(zanr);
            await _context.SaveChangesAsync();
            return Result.Sucess();
        }

        public async Task<Result> ObrisiZanrPoId(int id)
        {
            var zanr = await _context.Zanr.FindAsync(id);
            if (zanr == null)
            {
                return Result.Faliure(new Error("Error.NoData", "Nema zanra sa datim identifikatorom."));
            }
            _context.Zanr.Remove(zanr);
            await _context.SaveChangesAsync();
            return Result.Sucess();
        }
        private bool ZanrExists(int id)
        {
            return _context.Zanr.Any(e => e.ZanrId == id);
        }


    }
}
