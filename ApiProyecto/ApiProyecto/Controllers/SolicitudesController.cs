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
    public class SolicitudesController : ControllerBase
    {
        private readonly ProyectoMFEContext _context;

        public SolicitudesController(ProyectoMFEContext context)
        {
            _context = context;
        }

        // GET: api/Solicitudes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Solicitudes>>> GetSolicitudes()
        {
            return await _context.Solicitudes.ToListAsync();
        }

        // GET: api/Solicitudes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Solicitudes>> GetSolicitudes(string id)
        {
            var solicitudes = await _context.Solicitudes.FindAsync(id);

            if (solicitudes == null)
            {
                return NotFound();
            }

            return solicitudes;
        }

        // PUT: api/Solicitudes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSolicitudes(string id, Solicitudes solicitudes)
        {
            if (id != solicitudes.NumSerie)
            {
                return BadRequest();
            }

            _context.Entry(solicitudes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitudesExists(id))
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

        // POST: api/Solicitudes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Solicitudes>> PostSolicitudes(Solicitudes solicitudes)
        {
            _context.Solicitudes.Add(solicitudes);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SolicitudesExists(solicitudes.NumSerie))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSolicitudes", new { id = solicitudes.NumSerie }, solicitudes);
        }

        // DELETE: api/Solicitudes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolicitudes(string id)
        {
            var solicitudes = await _context.Solicitudes.FindAsync(id);
            if (solicitudes == null)
            {
                return NotFound();
            }

            _context.Solicitudes.Remove(solicitudes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SolicitudesExists(string id)
        {
            return _context.Solicitudes.Any(e => e.NumSerie == id);
        }
    }
}
