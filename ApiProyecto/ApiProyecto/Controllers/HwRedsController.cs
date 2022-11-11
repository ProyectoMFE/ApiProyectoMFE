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
    public class HwRedsController : ControllerBase
    {
        private readonly ProyectoMFEContext _context;

        public HwRedsController(ProyectoMFEContext context)
        {
            _context = context;
        }

        // GET: api/HwReds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HwRed>>> GetHwRed()
        {
            return await _context.HwRed.ToListAsync();
        }

        // GET: api/HwReds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HwRed>> GetHwRed(string id)
        {
            var hwRed = await _context.HwRed.FindAsync(id);

            if (hwRed == null)
            {
                return NotFound();
            }

            return hwRed;
        }

        // PUT: api/HwReds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHwRed(string id, HwRed hwRed)
        {
            if (id != hwRed.NumSerie)
            {
                return BadRequest();
            }

            _context.Entry(hwRed).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HwRedExists(id))
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

        // POST: api/HwReds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HwRed>> PostHwRed(HwRed hwRed)
        {
            _context.HwRed.Add(hwRed);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HwRedExists(hwRed.NumSerie))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHwRed", new { id = hwRed.NumSerie }, hwRed);
        }

        // DELETE: api/HwReds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHwRed(string id)
        {
            var hwRed = await _context.HwRed.FindAsync(id);
            if (hwRed == null)
            {
                return NotFound();
            }

            _context.HwRed.Remove(hwRed);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HwRedExists(string id)
        {
            return _context.HwRed.Any(e => e.NumSerie == id);
        }
    }
}
