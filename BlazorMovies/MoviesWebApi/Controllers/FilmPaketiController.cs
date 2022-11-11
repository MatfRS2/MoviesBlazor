using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesWebApi.Data;
using MoviesWebApi.Models;

namespace MoviesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmPaketiController : ControllerBase
    {
        private readonly MoviesWebApiContext _context;

        public FilmPaketiController(MoviesWebApiContext context)
        {
            _context = context;
        }

        // GET: api/FilmPaketi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmPaket>>> GetFilmPaket()
        {
            return await _context.FilmPaket
                //.Include(x => x.Film)
                //.Include(x => x.Paket)
                .ToListAsync();
        }
         
        // GET: api/FilmPaketi/5 7
        [HttpGet("{filmId}")]
        public async Task<ActionResult<FilmPaket>> GetFilmPaket(int filmId, int paketId)
        {
            var filmPaket = await _context.FilmPaket
                .Where(fp => fp.FilmId == filmId && fp.PaketId == paketId)
                //.Include(fp => fp.Film)
                //.Include(fp => fp.Paket)
                .SingleOrDefaultAsync();
            if (filmPaket == null)
            {
                return NotFound();
            }

            return filmPaket;
        }

        // PUT: api/FilmPaketi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilmPaket(int id, FilmPaket filmPaket)
        {
            if (id != filmPaket.FilmId)
            {
                return BadRequest();
            }

            _context.Entry(filmPaket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmPaketExists(id))
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

        // POST: api/FilmPaketi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FilmPaket>> PostFilmPaket(FilmPaket filmPaket)
        {
            _context.FilmPaket.Add(filmPaket);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FilmPaketExists(filmPaket.FilmId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFilmPaket", new { id = filmPaket.FilmId }, filmPaket);
        }

        // DELETE: api/FilmPaketi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilmPaket(int id)
        {
            var filmPaket = await _context.FilmPaket.FindAsync(id);
            if (filmPaket == null)
            {
                return NotFound();
            }

            _context.FilmPaket.Remove(filmPaket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FilmPaketExists(int id)
        {
            return _context.FilmPaket.Any(e => e.FilmId == id);
        }
    }
}
