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
    public class KorisniciController : ControllerBase
    {
        private readonly MoviesWebApiContext _context;

        public KorisniciController(MoviesWebApiContext context)
        {
            _context = context;
        }

        // GET: api/Korisnici
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Korisnik>>> GetKorisnik()
        {
            return await _context.Korisnik.ToListAsync();
        }

        // GET: api/Korisnici/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Korisnik>> GetKorisnik(int id)
        {
            var korisnik = await _context.Korisnik.FindAsync(id);

            if (korisnik == null)
            {
                return NotFound();
            }

            return korisnik;
        }

        // PUT: api/Korisnici/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKorisnik(int id, Korisnik korisnik)
        {
            if (id != korisnik.KorisnikId)
            {
                return BadRequest();
            }

            _context.Entry(korisnik).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KorisnikExists(id))
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

        // POST: api/Korisnici
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Korisnik>> PostKorisnik(Korisnik korisnik)
        {
            _context.Korisnik.Add(korisnik);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKorisnik", new { id = korisnik.KorisnikId }, korisnik);
        }

        // DELETE: api/Korisnici/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKorisnik(int id)
        {
            var korisnik = await _context.Korisnik.FindAsync(id);
            if (korisnik == null)
            {
                return NotFound();
            }

            _context.Korisnik.Remove(korisnik);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KorisnikExists(int id)
        {
            return _context.Korisnik.Any(e => e.KorisnikId == id);
        }
    }
}
