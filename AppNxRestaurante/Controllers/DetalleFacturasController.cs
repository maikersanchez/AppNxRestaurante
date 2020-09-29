using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppNxRestaurante.Context;
using AppNxRestaurante.Entities;
using AppNxRestaurante.Enums;

namespace AppNxRestaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleFacturasController : ControllerBase
    {
        private readonly DbRestauranteContext _context;

        public DetalleFacturasController(DbRestauranteContext context)
        {
            _context = context;
        }

        // GET: api/DetalleFacturas
        [HttpGet]
        public IEnumerable<TDetalleFactura> GetTDetalleFactura()
        {
            return _context.TDetalleFactura;
        }

        // GET: api/DetalleFacturas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTDetalleFactura([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tDetalleFactura = await _context.TDetalleFactura.FindAsync(id);

            if (tDetalleFactura == null)
            {
                return NotFound();
            }

            return Ok(tDetalleFactura);
        }

        // PUT: api/DetalleFacturas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTDetalleFactura([FromRoute] long id, [FromBody] TDetalleFactura tDetalleFactura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tDetalleFactura.IdDetalleFactura)
            {
                return BadRequest();
            }

            _context.Entry(tDetalleFactura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TDetalleFacturaExists(id))
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

        // POST: api/DetalleFacturas
        [HttpPost]
        public async Task<IActionResult> PostTDetalleFactura([FromBody] TDetalleFactura tDetalleFactura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tDetalleFactura.BActivo = (byte)Estados.EstadoEnum.Activo;
            tDetalleFactura.FCreacion = DateTime.Now;
            _context.TDetalleFactura.Add(tDetalleFactura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTDetalleFactura", new { id = tDetalleFactura.IdDetalleFactura }, tDetalleFactura);
        }

        // DELETE: api/DetalleFacturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTDetalleFactura([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tDetalleFactura = await _context.TDetalleFactura.FindAsync(id);
            if (tDetalleFactura == null)
            {
                return NotFound();
            }

            tDetalleFactura.BActivo = (byte)Estados.EstadoEnum.Inactivo;
            tDetalleFactura.FModificacion = DateTime.Now;
            _context.TDetalleFactura.Remove(tDetalleFactura);
            await _context.SaveChangesAsync();

            return Ok(tDetalleFactura);
        }

        private bool TDetalleFacturaExists(long id)
        {
            return _context.TDetalleFactura.Any(e => e.IdDetalleFactura == id);
        }
    }
}