using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sgc_backend.DTOs;
using sgc_backend.models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sgc_backend.Controllers
{
    [ApiController]
    [Route("api/producto")]
    public class ProductoController : ControllerBase
    {
        private readonly MyWebApiContext context;
        private readonly IMapper mapper;

        public ProductoController(MyWebApiContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet("obtenerProductos")]
        public async Task<ActionResult<List<Producto>>> ObtenerProductos()
        {
            var productos = await context.Producto.ToListAsync();
            return productos;
        }
        [HttpPost("crearProducto")]
        public async Task<ActionResult> CrearProducto([FromBody] ProductoDTO productoDTO)
        {
            try
            {
                Producto producto = mapper.Map<Producto>(productoDTO);
                context.Add(producto);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Error al agregar producto: " + ex.Message);
            }
        }
        [HttpPut("actualizarProducto/{idProducto}")]
        public async Task<ActionResult> ActualizarProducto(string idProducto, [FromBody] ProductoDTO productoDTO)
        {
            try
            {
                Producto producto = await context.Producto.FirstOrDefaultAsync(pro => pro.Id.Equals(idProducto));
                mapper.Map(productoDTO, producto);
                await context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest("Error al actuaizar producto: " + ex.Message);
            }
        }
        [HttpDelete("eliminarProducto/{idProducto}")]
        public async Task<ActionResult> EliminarProducto(string idProducto)
        {
            try
            {
                Producto producto = await context.Producto.FirstOrDefaultAsync(pro => pro.Id.Equals(idProducto));
                context.Producto.Remove(producto);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Error al aeliminar producto: " + ex.Message);
            }
        }
    }
}
