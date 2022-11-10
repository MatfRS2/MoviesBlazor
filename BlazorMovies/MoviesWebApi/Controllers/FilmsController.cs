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
    public class FilmsController : ControllerBase
    {
        private readonly MoviesWebApiContext _context;

        public FilmsController(MoviesWebApiContext context)
        {
            _context = context;
        }

        // GET: api/Films
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmGetDto>>> GetFilms()
        {
            List<Film> films = await _context.Film.Include(f => f.Zanr).ToListAsync();
            List<FilmGetDto> ret = new List<FilmGetDto>();
            foreach (var film in films)
                ret.Add(new FilmGetDto()
                {
                    FilmId = film.FilmId,
                    DatumPocetkaPrikazivanja = film.DatumPocetkaPrikazivanja,
                    Naslov = film.Naslov,
                    Ulozeno = film.Ulozeno,
                    ZanrId = film.ZanrId,
                    Zanr = new ZanrDto()
                    {
                        ZanrId = film.Zanr.ZanrId,
                        Naziv = film.Zanr.Naziv
                    }
                });
            return Ok(ret);
        }

        // GET: api/Films/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FilmGetDto>> GetFilm(int id)
        {
            var film = await _context.Film.Where(f=> f.FilmId == id).Include(f => f.Zanr).SingleOrDefaultAsync();

            if (film == null)
            {
                return NotFound();
            }

            return new FilmGetDto(){
                FilmId = film.FilmId,
                DatumPocetkaPrikazivanja = film.DatumPocetkaPrikazivanja,
                Naslov = film.Naslov,
                Ulozeno = film.Ulozeno, 
                ZanrId = film.ZanrId,
                Zanr = new ZanrDto()
                {
                    ZanrId = film.Zanr.ZanrId,
                    Naziv = film.Zanr.Naziv
                }
            };
        }

        // PUT: api/Films/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilm(int id, FilmPutDto filmDto)
        {
            if (id != filmDto.FilmId)
            {
                return BadRequest();
            }

            var film = await _context.Film.FindAsync(id);
            if (film == null)
                return NotFound();
            film.DatumPocetkaPrikazivanja = filmDto.DatumPocetkaPrikazivanja;
            film.Naslov = filmDto.Naslov;
            film.Ulozeno = filmDto.Ulozeno;
            film.ZanrId = filmDto.ZanrId;
            _context.Entry(film).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(id))
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

        // POST: api/Films
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Film>> PostFilm(FilmPostDto filmDto)
        {
            Film film = new Film()
            {
                Naslov = filmDto.Naslov,
                DatumPocetkaPrikazivanja = filmDto.DatumPocetkaPrikazivanja,
                Ulozeno = filmDto.Ulozeno,
                ZanrId = filmDto.ZanrId
            };
            _context.Film.Add(film);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilm", new { id = film.FilmId }, film);
        }

        // DELETE: api/Films/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            var film = await _context.Film.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }

            _context.Film.Remove(film);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FilmExists(int id)
        {
            return _context.Film.Any(e => e.FilmId == id);
        }
    }
}
