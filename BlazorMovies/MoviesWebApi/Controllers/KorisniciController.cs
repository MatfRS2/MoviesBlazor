using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesWebApi.Data;
using MoviesWebApi.Models;
using MoviesWebApi.Queries.VratiKorisnikSve;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KorisniciController : ApiController
    {
        private readonly MoviesWebApiContext _context;
        private readonly IMapper _mapper;

        public KorisniciController(ISender sender, MoviesWebApiContext context, IMapper mapper)
            : base(sender)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/Korisnici
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KorisnikGetDto>>> GetKorisnik(CancellationToken cancelationToken)
        {
            var query = new VratiKorisnikSveQuery();
            Result<List<Queries.VratiKorisnikSve.KorisnikResponse>> res = await _sender.Send(query, cancelationToken);
            return Ok(res.Value);
        }

        // GET: api/Korisnici/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KorisnikGetDto>> GetKorisnik(int id)
        {
            var korisnik = await _context.Korisnik.FindAsync(id);
            if (korisnik == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<KorisnikGetDto>(korisnik));
        }

        // PUT: api/Korisnici/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKorisnik(int id, KorisnikGetDto korisnik)
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
        public async Task<ActionResult<KorisnikGetDto>> PostKorisnik(Korisnik korisnik)
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
