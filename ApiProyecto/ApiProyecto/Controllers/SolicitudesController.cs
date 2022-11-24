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
using NuGet.DependencyResolver;

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

        /// <summary>
        /// Lista las solicitudes que hay en la base de datos.
        /// </summary>
        /// <param name="numSerie">Con esta opción puedes filtrar por número de serie.</param>
        /// <param name="id">Con esta opción puedes filtrar por id de usuario.</param>
        /// <returns>Un lista con las solicitudes.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Solicitudes>>> GetSolicitudes(string? numSerie, int? id)
        {
            List<Solicitudes> solicitudes;


            if (numSerie == null && id == null)
            {
                if (numSerie != null)
                {
                    solicitudes = _context.Solicitudes.Where(f => f.NumSerie == numSerie).ToList();
                }
                else if (id != null)
                {
                    solicitudes = _context.Solicitudes.Where(x => x.IdUsuario == id).ToList();
                }
                else
                {
                    return await _context.Solicitudes.ToListAsync();
                }
            }
            else
            {
                solicitudes = _context.Solicitudes.Where(f => f.NumSerie == numSerie).ToList();
                solicitudes.Where(f => f.IdUsuario == id);
            }
            

            return solicitudes;
        }

        /// <summary>
        /// Acepta o rechaza las solicitides.
        /// </summary>
        /// <param name="numSerie">El número de serie.</param>
        /// <param name="correo">El correo del usuario.</param>
        /// <param name="action">R: Rechazas la solicitud, A: Aceptas la solicitud.</param>
        /// <returns>Respuesta HTTP.</returns>
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

            try 
            {
                comando.ExecuteReader();
                return Ok("Modificado");
            } catch (Exception) 
            {
                return NotFound("No se pudo modificar la solicitud, quizá no exista");
            }
            finally
            {
                conexion.Close();
            }  
        }

        /// <summary>
        /// Inserta una solicitud.
        /// </summary>
        /// <param name="numSerie">El número de serie.</param>
        /// <param name="correo">El correo del usuario.</param>
        /// <returns>Respuesta HTTP.</returns>
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

            try
            {
                comando.ExecuteReader();
                return Ok("Insertado");
            }
            catch (Exception)
            {
                return NotFound("No se pudo insertar la solicitud, quizá ya exista");
            }
            finally
            {
                conexion.Close();
            }
        }

        /// <summary>
        /// Finaliza una solicitud que fué aceptada.
        /// </summary>
        /// <param name="numSerie">El número de serie.</param>
        /// <param name="correo">El correo del usuario.</param>
        /// <returns>Respuesta HTTP.</returns>
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

            try
            {
                comando.ExecuteReader();
                return Ok("Eliminado");
            }
            catch (Exception)
            {
                return NotFound("No se pudo insertar la solicitud, quizá no exista");
            }
            finally
            {
                conexion.Close();
            }
        }

        private bool SolicitudesExists(string id)
        {
            return _context.Solicitudes.Any(e => e.NumSerie == id);
        }
    }
}
