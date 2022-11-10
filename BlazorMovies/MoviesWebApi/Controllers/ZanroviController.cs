using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesWebApi.Data;
using MoviesWebApi.Models;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZanroviController : ControllerBase
    {
        private readonly MoviesWebApiContext _context;

        public ZanroviController(MoviesWebApiContext context)
        {
            _context = context;
        }

        // GET: api/Zanrovi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZanrDto>>> GetZanrovi()
        {
            List<Zanr> zanrovi = await _context.Zanr.ToListAsync();
            List<ZanrDto> ret = new List<ZanrDto>();
            foreach (var zanr in zanrovi)
                ret.Add(new ZanrDto()
                {
                    ZanrId = zanr.ZanrId,
                    Naziv = zanr.Naziv,
                });
            return Ok(ret);
        }

        // GET: api/Zanrovi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ZanrDto>> GetZanr(int id)
        {
            var zanr = await _context.Zanr.Where(z=> z.ZanrId == id).SingleOrDefaultAsync();

            if (zanr == null)
            {
                return NotFound();
            }

            return new ZanrDto(){
                ZanrId = zanr.ZanrId,
                Naziv = zanr.Naziv,
            };
        }

        // PUT: api/Zanrovi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZanr(int id, ZanrDto zanrDto)
        {
            if (id != zanrDto.ZanrId)
            {
                return BadRequest();
            }

            var zanr = await _context.Zanr.FindAsync(id);
            if (zanr == null)
                return NotFound();
            zanr.Naziv = zanrDto.Naziv;
            _context.Entry(zanr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZanrExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Zanrovi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Zanr>> PostZanr(ZanrDto zanrDto)
        {
            Zanr zanr = new Zanr()
            {
                ZanrId = zanrDto.ZanrId,
                Naziv = zanrDto.Naziv
            };
            _context.Zanr.Add(zanr);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZanrovi", new { id = zanr.ZanrId }, zanr);
        }

        // DELETE: api/Zanrovi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZanr(int id)
        {
            var zanr = await _context.Zanr.FindAsync(id);
            if (zanr == null)
            {
                return NotFound();
            }

            _context.Zanr.Remove(zanr);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ZanrExists(int id)
        {
            return _context.Zanr.Any(e => e.ZanrId == id);
        }
    }
}
