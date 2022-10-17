//using AutoMapper;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using sgc_backend.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Threading.Tasks;

//namespace sgc_backend.Controllers
//{
//    /// <summary>
//    /// Status HTTP:
//    /// 200 = Ok: Respuesta estándar para peticiones correctas. 
//    /// 202 = Accepted: La petición ha sido aceptada para procesamiento, pero este no ha sido completado. La petición eventualmente puede no haber sido satisfecha, ya que podría ser no permitida o prohibida cuando el procesamiento tenga lugar.
//    /// 204 = No Content: La petición se ha completado con éxito pero su respuesta no tiene ningún contenido (la respuesta puede incluir información en sus cabeceras HTTP).
//    /// 400 = Bad Request: El servidor no procesará la solicitud, porque no puede, o no debe, debido a algo que es percibido como un error del cliente (ej: solicitud malformada, sintaxis errónea, etc). La solicitud contiene sintaxis errónea y no debería repetirse.
//    /// 404 = Not Found: Recurso no encontrado. Se utiliza cuando el servidor web no encuentra la página o recurso solicitado.
//    /// </summary>
//    [ApiController]
//    [Route("api/fechaCambio")]
//    public class FechaCambioController : ControllerBase
//    {
//        private readonly MyWebApiContext context;
//        public FechaCambioController(MyWebApiContext context)
//        {
//            this.context = context;
//        }
//        [HttpGet("listaDeFechaCambio")]
//        public async Task<ActionResult<List<FechaCambio>>> ListaDeFechaCambio()
//        {
//            try
//            {
//                var fechaCambio = await context.FechaCambio.ToListAsync();
//                if (fechaCambio == null) return NotFound("No existen registros.");
//                return fechaCambio;
//            }
//            catch (Exception ex)
//            {
//                // Recurso no encontrado. Se utiliza cuando el servidor web no encuentra la página o recurso solicitado.
//                return BadRequest($"Ocurrió un error: {ex}");
//            }
//        }
//        [HttpGet("obtenerFechaCambio/{id}")]
//        public async Task<ActionResult<FechaCambio>> ObtenerFechaCambio(string id)
//        {
//            try
//            {
//                var fechaCambio = await context.FechaCambio.FirstOrDefaultAsync(x => id == x.Id.ToString());
//                if (fechaCambio == null) return NotFound("No existe el registro con el ID solicitado.");
//                return fechaCambio;
//            }
//            catch (Exception ex)
//            {
//                // Recurso no encontrado. Se utiliza cuando el servidor web no encuentra la página o recurso solicitado.
//                return BadRequest($"Ocurrió un error: {ex}");
//            }
//        }
//        [HttpDelete("eliminarTodo")]
//        public async Task<ActionResult> EliminarTodo()
//        {
//            try
//            {
//                IQueryable<FechaCambio> fechaCambio = context.FechaCambio.AsQueryable();
//                if (fechaCambio.Count() == 0) return NotFound("No hay registros para eliminar.");
//                context.FechaCambio.RemoveRange(fechaCambio);
//                await context.SaveChangesAsync();
//                return Ok("Se eliminaron todos los registros.");
//            }
//            catch (Exception ex)
//            {
//                // Recurso no encontrado. Se utiliza cuando el servidor web no encuentra la página o recurso solicitado.
//                return BadRequest($"Ocurrió un error: {ex}");
//            }
//        }
//    }
//}
