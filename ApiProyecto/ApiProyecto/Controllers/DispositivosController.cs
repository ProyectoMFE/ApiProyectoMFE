using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiProyecto.DataAccess;
using ApiProyecto.Models;
using ApiProyecto.Controllers;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="marca"></param>
        /// <param name="estado"></param>
        /// <param name="localizacion"></param>
        /// <param name="categoria"></param>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dispositivos>>> GetDispositivosLista(string? marca, string? estado, string? localizacion, string? categoria, string? modelo)
        {
            List<Dispositivos> dispositivos;

            if (marca != null)
            {
                dispositivos = _context.Dispositivos.Where(f => f.Marca == marca).ToList();
                
            } else if (estado != null)
            {
                dispositivos = _context.Dispositivos.Where(f => f.Estado == estado).ToList();
            } else if (localizacion != null)
            {
                dispositivos = _context.Dispositivos.Where(f => f.Localizacion == localizacion).ToList();
            } else if (categoria != null)
            {
                dispositivos = _context.Dispositivos.Where(f => f.IdCategoriaNavigation.Nombre == categoria).ToList();
            } else if (modelo != null)
            {
                dispositivos = _context.Dispositivos.Where(f => f.Modelo == modelo).ToList();
            } else
            {
                return await _context.Dispositivos.ToListAsync();
            }

            return dispositivos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dispositivos"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dispositivos"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
