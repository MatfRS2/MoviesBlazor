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
using MoviesWebApi.Operations;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZanroviController : ControllerBase
    {
        private readonly IZanroviOperations _operations;

        public ZanroviController(IZanroviOperations operations)
        {
            _operations = operations ?? throw new ArgumentNullException(nameof(operations));
        }

        // GET: api/Zanrovi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZanrDto>>> GetZanrovi()
        {
            var res = await _operations.SviZanrovi();
            if(!res.IsSucess)
                return NotFound(res.Error);
            return Ok(res.Value);
        }

        // GET: api/Zanrovi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ZanrDto>> GetZanr(int id)
        {
            var res = await _operations.ZanrPoId(id);
            if (!res.IsSucess)
                return NotFound(res.Error);
            var zanr = res.Value;
            return Ok(zanr);
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
            var res = await _operations.PostaviZanr(id, zanrDto);
            if (res.IsFaliure)
                return NotFound(res.Error);
            return NoContent();
        }

        // POST: api/Zanrovi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Zanr>> PostZanr(ZanrDto zanrDto)
        {
            var res = await _operations.DodajZanr(zanrDto);
            if (res.IsFaliure)
                return NotFound(res.Error);
            return CreatedAtAction("GetZanrovi", new { id = zanrDto.ZanrId }, zanrDto);
        }

        // DELETE: api/Zanrovi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZanr(int id)
        {
            var res = await _operations.ObrisiZanrPoId(id);
            if (res.IsFaliure)
                return NotFound(res.Error);
            return NoContent();
        }

    }
}
