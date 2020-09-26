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
    public class ClientesController : ControllerBase
    {
        private readonly DbRestauranteContext _context;

        public ClientesController(DbRestauranteContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public IEnumerable<TCliente> GetTCliente()
        {
            return _context.TCliente;
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTCliente([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tCliente = await _context.TCliente.FindAsync(id);

            if (tCliente == null)
            {
                return NotFound();
            }

            return Ok(tCliente);
        }

        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTCliente([FromRoute] int id, [FromBody] TCliente tCliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tCliente.IdCliente)
            {
                return BadRequest();
            }

            tCliente.FModificacion = DateTime.Now;
            _context.Entry(tCliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TClienteExists(id))
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

        // POST: api/Clientes
        [HttpPost]
        public async Task<IActionResult> PostTCliente([FromBody] TCliente tCliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tCliente.BActivo = (byte) Estados.EstadoEnum.Activo; 
            tCliente.FCreacion = DateTime.Now;
            _context.TCliente.Add(tCliente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TClienteExists(tCliente.IdCliente))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTCliente", new { id = tCliente.IdCliente }, tCliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTCliente([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tCliente = await _context.TCliente.FindAsync(id);
            if (tCliente == null)
            {
                return NotFound();
            }

            tCliente.BActivo = (byte)Estados.EstadoEnum.Inactivo;
            _context.Entry(tCliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(tCliente);
        }

        private bool TClienteExists(int id)
        {
            return _context.TCliente.Any(e => e.IdCliente == id);
        }
    }
}