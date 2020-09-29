using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppNxRestaurante.Context;
using AppNxRestaurante.Entities;

namespace AppNxRestaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesasController : ControllerBase
    {
        private readonly DbRestauranteContext _context;

        public MesasController(DbRestauranteContext context)
        {
            _context = context;
        }

        // GET: api/Mesas
        [HttpGet]
        public IEnumerable<TMesa> GetTMesa()
        {
            return _context.TMesa;
        }

        // GET: api/Mesas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTMesa([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tMesa = await _context.TMesa.FindAsync(id);

            if (tMesa == null)
            {
                return NotFound();
            }

            return Ok(tMesa);
        }

        // GET: api/Mesas/Autocomplete/5
        [HttpGet("Autocomplete/{id}")]
        public async Task<IActionResult> GetAutocompleteMesas([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<string> listMesas = (List<string>)await _context.TMesa.AsQueryable().Where(x => x.IdMesa.Contains(id)).Select(x => x.IdMesa).ToAsyncEnumerable().ToList();

            if (listMesas == null)
            {
                return NotFound();
            }

            return Ok(listMesas);
        }

        // PUT: api/Mesas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTMesa([FromRoute] string id, [FromBody] TMesa tMesa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tMesa.IdMesa)
            {
                return BadRequest();
            }

            _context.Entry(tMesa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TMesaExists(id))
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

        // POST: api/Mesas
        [HttpPost]
        public async Task<IActionResult> PostTMesa([FromBody] TMesa tMesa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TMesa.Add(tMesa);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TMesaExists(tMesa.IdMesa))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTMesa", new { id = tMesa.IdMesa }, tMesa);
        }

        // DELETE: api/Mesas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTMesa([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tMesa = await _context.TMesa.FindAsync(id);
            if (tMesa == null)
            {
                return NotFound();
            }

            _context.TMesa.Remove(tMesa);
            await _context.SaveChangesAsync();

            return Ok(tMesa);
        }

        private bool TMesaExists(string id)
        {
            return _context.TMesa.Any(e => e.IdMesa == id);
        }
    }
}