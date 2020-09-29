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

        // GET: api/Camareros/Autocomplete/5
        [HttpGet("Autocomplete/{id}")]
        public async Task<IActionResult> GetAutocompleteCamareros([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<int> listCamareros = (List<int>)await _context.TCamarero.AsQueryable().Where(x => x.IdCamarero.ToString().Contains(id.ToString())).Select(x => x.IdCamarero).ToAsyncEnumerable().ToList();

            if (listCamareros == null)
            {
                return NotFound();
            }

            return Ok(listCamareros);
        }

        // GET: api/Camareros/Reporte-Mes/mes
        [HttpGet("Reporte-Mes/{mes}")]
        public async Task<IActionResult> GetReporteMesCamareros([FromRoute] int mes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tCamareroReporte = new List<CamareroReporteDto>();

            _context.TCamarero.Include(x => x.TFactura).Include("TFactura.TDetalleFactura").ToList().ForEach(x => {
            tCamareroReporte.Add(new CamareroReporteDto{
                IdCamarero = x.IdCamarero.ToString(),
                    VNombre = x.VNombre,
                    VApellido1 = x.VApellido1,
                    VApellido2 = x.VApellido2,
                    Month = mes.ToString(),
                    totalVentas = x.TFactura.Where(j => j.FFactura.Month == mes).Select(s => s.TDetalleFactura).Select(s => s.Sum(i => i.DImporte)).Sum()
                });
            });

            if (tCamareroReporte == null)
            {
                return NotFound();
            }

            return Ok(tCamareroReporte);
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

            tCamarero.FModificacion = DateTime.Now;
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
            tCamarero.BActivo = (byte)Estados.EstadoEnum.Activo;
            tCamarero.FCreacion = DateTime.Now;
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
            tCamarero.BActivo = (byte)Estados.EstadoEnum.Inactivo;
            _context.Entry(tCamarero).State = EntityState.Modified;
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