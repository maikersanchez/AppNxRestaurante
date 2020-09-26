﻿using System;
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