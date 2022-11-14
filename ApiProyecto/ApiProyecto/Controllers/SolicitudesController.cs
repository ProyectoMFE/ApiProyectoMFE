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
            SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand comando = conexion.CreateCommand();

            conexion.Open();
            comando.CommandType = System.Data.CommandType.StoredProcedure;

            if (action == 'R')
            {
                comando.CommandText = "RECHAZAR_SOLICITUD";
            }
            else if (action == 'A')
            {
                comando.CommandText = "ACEPTAR_SOLICITUD";
            }
            else
            {
                return NotFound("Accion inválida");
            }

            comando.Parameters.Add("@correo", System.Data.SqlDbType.VarChar, 30).Value = correo;
            comando.Parameters.Add("@num_dispositivo", System.Data.SqlDbType.VarChar, 20).Value = numSerie;

            comando.ExecuteReader();

            return Ok("Modificado");
        }

        // POST: api/Solicitudes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostSolicitudes(string numSerie, string correo)
        {
            SqlConnection conexion = (SqlConnection) _context.Database.GetDbConnection();
            SqlCommand comando = conexion.CreateCommand();

            conexion.Open();
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = "INSERTAR_SOLICITUD";

            comando.Parameters.Add("@correo", System.Data.SqlDbType.VarChar, 30).Value = correo;
            comando.Parameters.Add("@num_dispositivo", System.Data.SqlDbType.VarChar, 20).Value = numSerie;

            comando.ExecuteReader();

            conexion.Close();

            return Ok("Insertado");
        }

        // DELETE: api/Solicitudes/5
        [HttpDelete()]
        public async Task<IActionResult> DeleteSolicitudes(string numSerie, string correo)
        {
            SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand comando = conexion.CreateCommand();

            conexion.Open();
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = "FINALIZAR_SOLICITUD";

            comando.Parameters.Add("@correo", System.Data.SqlDbType.VarChar, 30).Value = correo;
            comando.Parameters.Add("@num_dispositivo", System.Data.SqlDbType.VarChar, 20).Value = numSerie;

            comando.ExecuteReader();

            return Ok("Eliminado");
        }

        private bool SolicitudesExists(string id)
        {
            return _context.Solicitudes.Any(e => e.NumSerie == id);
        }
    }
}
