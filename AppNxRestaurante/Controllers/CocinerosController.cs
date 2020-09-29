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
using AppNxRestaurante.Dto;

namespace AppNxRestaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CocinerosController : ControllerBase
    {
        private readonly DbRestauranteContext _context;

        public CocinerosController(DbRestauranteContext context)
        {
            _context = context;
        }

        // GET: api/Cocineros
        [HttpGet]
        public IEnumerable<TCocinero> GetTCocinero()
        {
            return _context.TCocinero;
        }

        // GET: api/Cocineros/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTCocinero([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tCocinero = await _context.TCocinero.FindAsync(id);

            if (tCocinero == null)
            {
                return NotFound();
            }

            return Ok(tCocinero);
        }

        // GET: api/Cocineros/Reporte-Mes/mes
        [HttpGet("Reporte-Mes/{mes}")]
        public async Task<IActionResult> GetReporteMesCocineros([FromRoute] int mes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tCocineroReporte  = _context.TCocinero.GroupJoin(_context.TDetalleFactura, co => new { co.IdCocinero }, df => new { df.IdCocinero }, (co, df) => new { co.IdCocinero, co.VNombre, co.VApellido1, co.VApellido2, df.FirstOrDefault().IdFacturaNavigation.FFactura.Month, df.FirstOrDefault().DImporte})
                .GroupBy(g => new { g.IdCocinero, g.VNombre, g.VApellido1, g.VApellido2, g.Month })
                .Select(s => new CocineroReporteDto(){
                    IdCocinero = s.Key.IdCocinero.ToString(),
                    VNombre = s.Key.VNombre,
                    VApellido1 = s.Key.VApellido1,
                    VApellido2 = s.Key.VApellido2,
                    Month = s.Key.Month.ToString(),
                    totalVentas = s.Sum( x => x.DImporte)
                });

            if (tCocineroReporte == null)
            {
                return NotFound();
            }

            return Ok(tCocineroReporte);
        }


        // GET: api/Cocineros/Autocomplete/5
        [HttpGet("Autocomplete/{id}")]
        public async Task<IActionResult> GetAutocompleteCocineros([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<int> listCocineros = (List<int>)await _context.TCocinero.AsQueryable().Where(x => x.IdCocinero.ToString().Contains(id.ToString())).Select(x => x.IdCocinero).ToAsyncEnumerable().ToList();

            if (listCocineros == null)
            {
                return NotFound();
            }

            return Ok(listCocineros);
        }


        // PUT: api/Cocineros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTCocinero([FromRoute] int id, [FromBody] TCocinero tCocinero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tCocinero.IdCocinero)
            {
                return BadRequest();
            }

            _context.Entry(tCocinero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TCocineroExists(id))
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

        // POST: api/Cocineros
        [HttpPost]
        public async Task<IActionResult> PostTCocinero([FromBody] TCocinero tCocinero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tCocinero.BActivo = (byte)Estados.EstadoEnum.Activo;
            tCocinero.FCreacion = DateTime.Now;
            _context.TCocinero.Add(tCocinero);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TCocineroExists(tCocinero.IdCocinero))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTCocinero", new { id = tCocinero.IdCocinero }, tCocinero);
        }

        // DELETE: api/Cocineros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTCocinero([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tCocinero = await _context.TCocinero.FindAsync(id);
            if (tCocinero == null)
            {
                return NotFound();
            }

            tCocinero.BActivo = (byte)Estados.EstadoEnum.Inactivo;
            _context.Entry(tCocinero).State = EntityState.Modified;
            _context.TCocinero.Remove(tCocinero);
            await _context.SaveChangesAsync();

            return Ok(tCocinero);
        }

        private bool TCocineroExists(int id)
        {
            return _context.TCocinero.Any(e => e.IdCocinero == id);
        }
    }
}