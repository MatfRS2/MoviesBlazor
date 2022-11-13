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
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZanroviController : ControllerBase
    {
        private readonly ZanroviOperations _operations;

        public ZanroviController(ZanroviOperations operations)
        {
            _operations = operations ?? throw new ArgumentNullException(nameof(operations));
        }

        // GET: api/Zanrovi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZanrDto>>> GetZanrovi()
        {
            List<ZanrDto> ret = await _operations.SviZanrovi();
            return Ok(ret);
        }

        // GET: api/Zanrovi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ZanrDto>> GetZanr(int id)
        {
            var zanr = await _operations.ZanrPoId(id);
            if (zanr.ZanrId == -1 && zanr.Naziv == String.Empty)
                return NotFound();
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
            int ret = await _operations.PostaviZanr(id, zanrDto);
            if (ret < 0)
                return NotFound();
            return NoContent();
        }

        // POST: api/Zanrovi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Zanr>> PostZanr(ZanrDto zanrDto)
        {
            await _operations.DodajZanr(zanrDto);
            return CreatedAtAction("GetZanrovi", new { id = zanrDto.ZanrId }, zanrDto);
        }

        // DELETE: api/Zanrovi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZanr(int id)
        {
            int ret = await _operations.ObrisiZanrPoId(id);
            if (ret < 0)
                return NotFound();
            return NoContent();
        }

    }
}
