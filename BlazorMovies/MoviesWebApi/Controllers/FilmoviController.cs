using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class FilmoviController : ControllerBase
    {
        private readonly MoviesWebApiContext _context;
        private readonly IMapper _mapper;

        public FilmoviController(MoviesWebApiContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/Filmovi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmGetDto>>> GetFilms()
        {
            List<Film> films = await _context.Film
                .Include(f => f.Zanr)
                .ToListAsync();
            List<FilmGetDto> ret = _mapper.Map<List<Film>, List<FilmGetDto>>(films);
            return Ok(ret);
        }

        // GET: api/Filmovi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FilmGetDto>> GetFilm(int id)
        {
            var film = await _context.Film
                .Where(f=> f.FilmId == id)
                .Include(f => f.Zanr)
                .SingleOrDefaultAsync();
            if (film == null)
            {
                return NotFound();
            }
            return _mapper.Map<FilmGetDto>(film);
        }

        // PUT: api/Filmovi/5
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
            _mapper.Map<FilmPutDto, Film>(filmDto, film);
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

        // POST: api/Filmovi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Film>> PostFilm(FilmPostDto filmDto)
        {
            Film film = _mapper.Map<Film>(filmDto);
            _context.Film.Add(film);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilm", new { id = film.FilmId }, film);
        }

        // DELETE: api/Filmovi/5
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
