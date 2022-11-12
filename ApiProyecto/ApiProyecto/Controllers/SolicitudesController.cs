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
using Microsoft.Data.SqlClient;

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
        [HttpPut()]
        public async Task<IActionResult> PutSolicitudes(string numSerie, string correo, char action)
        {
            if (action == 'A')
            {
                var baseItem = _context.Solicitudes.FromSqlRaw("Execute dbo.ACEPTAR_SOLICITUD @correo={0}, @num_dispositivo={1}", correo, numSerie).ToList();
            }
            else if (action == 'A')
            {
                var baseItem = _context.Solicitudes.FromSqlRaw("Execute dbo.RECHAZAR_SOLICITUD @correo={0}, @num_dispositivo={1}", correo, numSerie).ToList();
            }

            return CreatedAtAction("GetOrdenadores", "si");
        }

        // POST: api/Solicitudes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Solicitudes>> PostSolicitudes(string numSerie, string correo)
        {
            var baseItem = _context.Solicitudes.FromSqlRaw("Execute dbo.INSERTAR_SOLICITUD @correo={0}, @num_dispositivo={1}", correo, numSerie).ToList();

            return NoContent();
        }

        // DELETE: api/Solicitudes/5
        [HttpDelete()]
        public async Task<IActionResult> DeleteSolicitudes(string numSerie, string correo)
        {
            var baseItem = _context.Solicitudes.FromSqlRaw("Execute dbo.FINALIZAR_SOLICITUD @correo={0}, @num_dispositivo={1}", correo, numSerie).ToList();

            return NoContent();
        }

        private bool SolicitudesExists(string id)
        {
            return _context.Solicitudes.Any(e => e.NumSerie == id);
        }
    }
}
