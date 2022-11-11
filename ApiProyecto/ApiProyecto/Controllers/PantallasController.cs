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
    public class PantallasController : ControllerBase
    {
        private readonly ProyectoMFEContext _context;

        public PantallasController(ProyectoMFEContext context)
        {
            _context = context;
        }

        // GET: api/Pantallas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pantallas>>> GetPantallas()
        {
            return await _context.Pantallas.ToListAsync();
        }

        // GET: api/Pantallas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pantallas>> GetPantallas(string id)
        {
            var pantallas = await _context.Pantallas.FindAsync(id);

            if (pantallas == null)
            {
                return NotFound();
            }

            return pantallas;
        }

        // PUT: api/Pantallas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPantallas(string id, Pantallas pantallas)
        {
            if (id != pantallas.NumSerie)
            {
                return BadRequest();
            }

            _context.Entry(pantallas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PantallasExists(id))
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

        // POST: api/Pantallas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pantallas>> PostPantallas(Pantallas pantallas)
        {
            _context.Pantallas.Add(pantallas);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PantallasExists(pantallas.NumSerie))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPantallas", new { id = pantallas.NumSerie }, pantallas);
        }

        // DELETE: api/Pantallas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePantallas(string id)
        {
            var pantallas = await _context.Pantallas.FindAsync(id);
            if (pantallas == null)
            {
                return NotFound();
            }

            _context.Pantallas.Remove(pantallas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PantallasExists(string id)
        {
            return _context.Pantallas.Any(e => e.NumSerie == id);
        }
    }
}
