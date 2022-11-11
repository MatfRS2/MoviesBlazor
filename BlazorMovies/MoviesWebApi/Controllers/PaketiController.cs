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
    public class PaketiController : ControllerBase
    {
        private readonly MoviesWebApiContext _context;

        public PaketiController(MoviesWebApiContext context)
        {
            _context = context;
        }

        // GET: api/Paketi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paket>>> GetPaket()
        {
            return await _context.Paket.ToListAsync();
        }

        // GET: api/Paketi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Paket>> GetPaket(int id)
        {
            var paket = await _context.Paket.FindAsync(id);

            if (paket == null)
            {
                return NotFound();
            }

            return paket;
        }

        // PUT: api/Paketi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaket(int id, Paket paket)
        {
            if (id != paket.PaketId)
            {
                return BadRequest();
            }

            _context.Entry(paket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaketExists(id))
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

        // POST: api/Paketi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Paket>> PostPaket(Paket paket)
        {
            _context.Paket.Add(paket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaket", new { id = paket.PaketId }, paket);
        }

        // DELETE: api/Paketi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaket(int id)
        {
            var paket = await _context.Paket.FindAsync(id);
            if (paket == null)
            {
                return NotFound();
            }

            _context.Paket.Remove(paket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaketExists(int id)
        {
            return _context.Paket.Any(e => e.PaketId == id);
        }
    }
}
