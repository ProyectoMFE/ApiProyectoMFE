using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiProyecto.DataAccess;
using ApiProyecto.Models;

namespace ApiProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenadoresController : ControllerBase
    {
        private readonly ProyectoMFEContext _context;

        public OrdenadoresController(ProyectoMFEContext context)
        {
            _context = context;
        }

        // GET: api/Ordenadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ordenadores>>> GetOrdenadores()
        {
            return await _context.Ordenadores.ToListAsync();
        }

        // GET: api/Ordenadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ordenadores>> GetOrdenadores(string id)
        {
            var ordenadores = await _context.Ordenadores.FindAsync(id);

            if (ordenadores == null)
            {
                return NotFound();
            }

            return ordenadores;
        }

        // PUT: api/Ordenadores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdenadores(string id, Ordenadores ordenadores)
        {
            if (id != ordenadores.NumSerie)
            {
                return BadRequest();
            }

            _context.Entry(ordenadores).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenadoresExists(id))
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

        // POST: api/Ordenadores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ordenadores>> PostOrdenadores(Ordenadores ordenadores)
        {
            _context.Ordenadores.Add(ordenadores);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrdenadoresExists(ordenadores.NumSerie))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrdenadores", new { id = ordenadores.NumSerie }, ordenadores);
        }

        // DELETE: api/Ordenadores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdenadores(string id)
        {
            var ordenadores = await _context.Ordenadores.FindAsync(id);
            if (ordenadores == null)
            {
                return NotFound();
            }

            _context.Ordenadores.Remove(ordenadores);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdenadoresExists(string id)
        {
            return _context.Ordenadores.Any(e => e.NumSerie == id);
        }
    }
}
