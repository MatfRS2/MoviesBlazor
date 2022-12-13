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
using MoviesWebApi.Queries.VratiKorisnikPoId;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;
using MoviesWebApi.Commands.Korisnici;
using MoviesWebApi.Commands.Zanrovi;

namespace MoviesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KorisniciController : ApiController
    {
        public KorisniciController(ISender sender, MoviesWebApiContext context, IMapper mapper)
            : base(sender)
        {
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
        public async Task<ActionResult<KorisnikGetDto>> GetKorisnik(int id, CancellationToken cancelationToken)
        {
            var query = new VratiKorisnikPoIdQuery(id);
            Result<Queries.VratiKorisnikPoId.KorisnikResponse> res = await _sender.Send(query, cancelationToken);
            if (!res.IsSucess)
                return NotFound(res.Error);
            return Ok(res.Value);
        }

        // PUT: api/Korisnici/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKorisnik(int id, KorisnikGetDto korisnik, CancellationToken cancelationToken)
        {
            if (id != korisnik.KorisnikId)
            {
                return BadRequest();
            }
            var command = new KorisnikPostaviCommand(id, korisnik.KorisnikId, korisnik.Email,
                korisnik.Ime, korisnik.Prezime, korisnik.Potroseno);
            var res = await _sender.Send(command, cancelationToken);
            if (res.IsFaliure)
                return BadRequest(res.Error);
            return NoContent();
        }

        // POST: api/Korisnici
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KorisnikGetDto>> PostKorisnik(Korisnik korisnik, CancellationToken cancelationToken)
        {
            var command = new KorisnikDodajCommand(korisnik.KorisnikId, korisnik.Email, 
                korisnik.Ime, korisnik.Prezime, korisnik.Potroseno);
            var res = await _sender.Send(command, cancelationToken);
            if (res.IsFaliure)
                return BadRequest(res.Error);
            return CreatedAtAction("GetKorisnik", new { id = korisnik.KorisnikId }, korisnik);
        }

        // DELETE: api/Korisnici/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKorisnik(int id, CancellationToken cancelationToken)
        {
            var command = new Commands.Korisnici.KorisnikObrisiCommand(id);
            var res = await _sender.Send(command, cancelationToken);
            if (res.IsFaliure)
                return BadRequest(res.Error);
            return NoContent();
        }

    }
}
