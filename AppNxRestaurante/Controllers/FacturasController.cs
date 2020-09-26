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
    public class FacturasController : ControllerBase
    {
        private readonly DbRestauranteContext _context;

        public FacturasController(DbRestauranteContext context)
        {
            _context = context;
        }

        // GET: api/Facturas
        [HttpGet]
        public IEnumerable<TFactura> GetTFactura()
        {
            return _context.TFactura;
        }

        // GET: api/Facturas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTFactura([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tFactura = await _context.TFactura.FindAsync(id);

            if (tFactura == null)
            {
                return NotFound();
            }

            return Ok(tFactura);
        }

        // PUT: api/Facturas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTFactura([FromRoute] long id, [FromBody] TFactura tFactura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tFactura.IdFactura)
            {
                return BadRequest();
            }

            _context.Entry(tFactura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TFacturaExists(id))
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

        // POST: api/Facturas
        [HttpPost]
        public async Task<IActionResult> PostTFactura([FromBody] TFactura tFactura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TFactura.Add(tFactura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTFactura", new { id = tFactura.IdFactura }, tFactura);
        }

        // DELETE: api/Facturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTFactura([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tFactura = await _context.TFactura.FindAsync(id);
            if (tFactura == null)
            {
                return NotFound();
            }

            _context.TFactura.Remove(tFactura);
            await _context.SaveChangesAsync();

            return Ok(tFactura);
        }

        private bool TFacturaExists(long id)
        {
            return _context.TFactura.Any(e => e.IdFactura == id);
        }
    }
}