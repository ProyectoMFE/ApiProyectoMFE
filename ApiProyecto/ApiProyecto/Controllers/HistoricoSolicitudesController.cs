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

        private bool HistoricoSolicitudesExists(string id)
        {
            return _context.HistoricoSolicitudes.Any(e => e.NumSerie == id);
        }
    }
}
