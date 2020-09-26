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
    public class CamarerosController : ControllerBase
    {
        private readonly DbRestauranteContext _context;

        public CamarerosController(DbRestauranteContext context)
        {
            _context = context;
        }

        // GET: api/Camareros
        [HttpGet]
        public IEnumerable<TCamarero> GetTCamarero()
        {
            return _context.TCamarero;
        }

        // GET: api/Camareros/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTCamarero([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tCamarero = await _context.TCamarero.FindAsync(id);

            if (tCamarero == null)
            {
                return NotFound();
            }

            return Ok(tCamarero);
        }

        // PUT: api/Camareros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTCamarero([FromRoute] int id, [FromBody] TCamarero tCamarero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tCamarero.IdCamarero)
            {
                return BadRequest();
            }

            _context.Entry(tCamarero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TCamareroExists(id))
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

        // POST: api/Camareros
        [HttpPost]
        public async Task<IActionResult> PostTCamarero([FromBody] TCamarero tCamarero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TCamarero.Add(tCamarero);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TCamareroExists(tCamarero.IdCamarero))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTCamarero", new { id = tCamarero.IdCamarero }, tCamarero);
        }

        // DELETE: api/Camareros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTCamarero([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tCamarero = await _context.TCamarero.FindAsync(id);
            if (tCamarero == null)
            {
                return NotFound();
            }

            _context.TCamarero.Remove(tCamarero);
            await _context.SaveChangesAsync();

            return Ok(tCamarero);
        }

        private bool TCamareroExists(int id)
        {
            return _context.TCamarero.Any(e => e.IdCamarero == id);
        }
    }
}