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
    public class DispositivosController : ControllerBase
    {
        private readonly ProyectoMFEContext _context;

        public DispositivosController(ProyectoMFEContext context)
        {
            _context = context;
        }

        // GET: api/Dispositivos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dispositivos>>> GetDispositivos()
        {
            return await _context.Dispositivos.ToListAsync();
        }

        // GET: api/Dispositivos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dispositivos>> GetDispositivos(string id)
        {
            var dispositivos = await _context.Dispositivos.FindAsync(id);

            if (dispositivos == null)
            {
                return NotFound();
            }

            return dispositivos;
        }

        // PUT: api/Dispositivos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDispositivos(string id, Dispositivos dispositivos)
        {
            if (id != dispositivos.NumSerie)
            {
                return BadRequest();
            }

            _context.Entry(dispositivos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DispositivosExists(id))
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

        // POST: api/Dispositivos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dispositivos>> PostDispositivos(Dispositivos dispositivos)
        {
            _context.Dispositivos.Add(dispositivos);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DispositivosExists(dispositivos.NumSerie))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDispositivos", new { id = dispositivos.NumSerie }, dispositivos);
        }

        // DELETE: api/Dispositivos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDispositivos(string id)
        {
            var dispositivos = await _context.Dispositivos.FindAsync(id);
            if (dispositivos == null)
            {
                return NotFound();
            }

            _context.Dispositivos.Remove(dispositivos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DispositivosExists(string id)
        {
            return _context.Dispositivos.Any(e => e.NumSerie == id);
        }
    }
}
