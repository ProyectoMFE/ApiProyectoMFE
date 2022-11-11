using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiProyecto.DataAccess;
using ApiProyecto.Models;
using Newtonsoft.Json;
using System.Net.Http;

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
        public async Task<IActionResult> PutSolicitudes(string id, string correo, Solicitudes solicitudes)
        {
            if (id != solicitudes.NumSerie)
            {
                return BadRequest();
            }

            string json = await new StreamReader(Request.Body).ReadToEndAsync();

            //Solicitudes.Autor Autor = JsonConvert.DeserializeObject<AccesoDatos.Autor>(json);

            var baseItem = _context.Solicitudes.FromSqlRaw("Execute dbo.INSERTAR_SOLICITUD @correo={0}, @num_dispositivo={1}", id, correo);

            return NoContent();
        }

        // POST: api/Solicitudes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Solicitudes>> PostSolicitudes(string id, string correo, Solicitudes solicitudes)
        {
            //HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);

            var baseItem = _context.Solicitudes.FromSqlRaw("Execute dbo.INSERTAR_SOLICITUD @correo={0}, @num_dispositivo={1}", id, correo);

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
