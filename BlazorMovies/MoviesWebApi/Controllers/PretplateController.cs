using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class PretplateController : ControllerBase
    {
        private readonly MoviesWebApiContext _context;
        private readonly IMapper _mapper;

        public PretplateController(MoviesWebApiContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/Pretplate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PretplataGetDto>>> GetPretplata()
        {
            var lista = await _context.Pretplata
                .Include(x => x.Korisnik)
                .Include(x => x.Paket)
                .ToListAsync();
            return Ok(_mapper.Map<List<Pretplata>, List<PretplataGetDto>>(lista));
        }

        // GET: api/Pretplate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PretplataGetDto>> GetPretplata(int id)
        {
            var pretplata = await _context.Pretplata
                .Where( x => x.PretplataId == id)
                .Include(x => x.Korisnik)
                .Include(x => x.Paket)
                .SingleAsync();
            if (pretplata == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PretplataGetDto>(pretplata));
        }

        // PUT: api/Pretplate/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPretplata(int id, Pretplata pretplata)
        {
            if (id != pretplata.PretplataId)
            {
                return BadRequest();
            }
            _context.Entry(pretplata).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PretplataExists(id))
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

        // POST: api/Pretplate
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pretplata>> PostPretplata(Pretplata pretplata)
        {
            _context.Pretplata.Add(pretplata);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPretplata", new { id = pretplata.PretplataId }, pretplata);
        }

        // DELETE: api/Pretplate/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePretplata(int id)
        {
            var pretplata = await _context.Pretplata.FindAsync(id);
            if (pretplata == null)
            {
                return NotFound();
            }
            _context.Pretplata.Remove(pretplata);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool PretplataExists(int id)
        {
            return _context.Pretplata.Any(e => e.PretplataId == id);
        }
    }
}
