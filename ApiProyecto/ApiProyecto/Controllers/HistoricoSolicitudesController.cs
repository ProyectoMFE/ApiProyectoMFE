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
    public class HistoricoSolicitudesController : ControllerBase
    {
        private readonly ProyectoMFEContext _context;

        public HistoricoSolicitudesController(ProyectoMFEContext context)
        {
            _context = context;
        }

        // GET: api/HistoricoSolicitudes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoricoSolicitudes>>> GetHistoricoSolicitudes()
        {
            return await _context.HistoricoSolicitudes.ToListAsync();
        }

        // GET: api/HistoricoSolicitudes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistoricoSolicitudes>> GetHistoricoSolicitudes(string id)
        {
            var historicoSolicitudes = await _context.HistoricoSolicitudes.FindAsync(id);

            if (historicoSolicitudes == null)
            {
                return NotFound();
            }

            return historicoSolicitudes;
        }

        // PUT: api/HistoricoSolicitudes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoricoSolicitudes(string id, HistoricoSolicitudes historicoSolicitudes)
        {
            if (id != historicoSolicitudes.NumSerie)
            {
                return BadRequest();
            }

            _context.Entry(historicoSolicitudes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoricoSolicitudesExists(id))
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

        // POST: api/HistoricoSolicitudes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HistoricoSolicitudes>> PostHistoricoSolicitudes(HistoricoSolicitudes historicoSolicitudes)
        {
            _context.HistoricoSolicitudes.Add(historicoSolicitudes);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HistoricoSolicitudesExists(historicoSolicitudes.NumSerie))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHistoricoSolicitudes", new { id = historicoSolicitudes.NumSerie }, historicoSolicitudes);
        }

        // DELETE: api/HistoricoSolicitudes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoricoSolicitudes(string id)
        {
            var historicoSolicitudes = await _context.HistoricoSolicitudes.FindAsync(id);
            if (historicoSolicitudes == null)
            {
                return NotFound();
            }

            _context.HistoricoSolicitudes.Remove(historicoSolicitudes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistoricoSolicitudesExists(string id)
        {
            return _context.HistoricoSolicitudes.Any(e => e.NumSerie == id);
        }
    }
}
