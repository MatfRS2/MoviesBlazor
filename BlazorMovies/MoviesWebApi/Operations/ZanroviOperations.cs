using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviesWebApi.Data;
using MoviesWebApi.Models;
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

        public async Task<List<ZanrDto>> SviZanrovi()
        {
            List<Zanr> zanrovi = await _context.Zanr.ToListAsync();
            List<ZanrDto> ret = _mapper.Map<List<ZanrDto>>(zanrovi);
            return ret;
        }

        public async Task<ZanrDto> ZanrPoId(int id)
        {
            var zanr = await _context.Zanr.Where(z => z.ZanrId == id).SingleOrDefaultAsync();
            if (zanr == null)
            {
                return new ZanrDto() { ZanrId = -1, Naziv = String.Empty};
            }
            return _mapper.Map<ZanrDto>(zanr);
        }


        public async Task<int> PostaviZanr(int id, ZanrDto zanrDto)
        {
            var zanr = await _context.Zanr.FindAsync(id);
            if (zanr == null)
                return -1;
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
                    return -1;
                }
                else
                {
                    throw;
                }
            }
            return 0;
        }

        public async Task DodajZanr(ZanrDto zanrDto)
        {
            Zanr zanr = _mapper.Map<Zanr>(zanrDto);
            _context.Zanr.Add(zanr);
            await _context.SaveChangesAsync();
        }

        public async Task<int> ObrisiZanrPoId(int id)
        {
            var zanr = await _context.Zanr.FindAsync(id);
            if (zanr == null)
            {
                return -1;
            }
            _context.Zanr.Remove(zanr);
            await _context.SaveChangesAsync();
            return 0;
        }
        private bool ZanrExists(int id)
        {
            return _context.Zanr.Any(e => e.ZanrId == id);
        }


    }
}
