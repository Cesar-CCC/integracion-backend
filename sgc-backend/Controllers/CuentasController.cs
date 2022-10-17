using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using sgc_backend.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using sgc_backend.Utils;
using System.Web;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using System.Net.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace sgc_backend.Controllers
{
    // Periodo admin: admin-sin-periodo
    // --------------------------------------------
    /// <summary>
    /// Status HTTP:
    /// 200 = Ok: Respuesta estándar para peticiones correctas. 
    /// 202 = Accepted: La petición ha sido aceptada para procesamiento, pero este no ha sido completado. La petición eventualmente puede no haber sido satisfecha, ya que podría ser no permitida o prohibida cuando el procesamiento tenga lugar.
    /// 204 = No Content: La petición se ha completado con éxito pero su respuesta no tiene ningún contenido (la respuesta puede incluir información en sus cabeceras HTTP).
    /// 400 = Bad Request: El servidor no procesará la solicitud, porque no puede, o no debe, debido a algo que es percibido como un error del cliente (ej: solicitud malformada, sintaxis errónea, etc). La solicitud contiene sintaxis errónea y no debería repetirse.
    /// 404 = Not Found: Recurso no encontrado. Se utiliza cuando el servidor web no encuentra la página o recurso solicitado.
    /// IMPORTANTE: 
    /// EL ID de la tabla USER solo se esta utilizando al momento de registrar, luego no se podrá obtener este ID.
    /// </summary>
    [ApiController]
    [Route("api/cuentas")]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;

        public CuentasController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            //this.signInManager = signInManager;
        }
        //--------------------------------------------------
        [HttpPost("registrarCuenta")]
        public async Task<ActionResult<RespuestaAutenticacion>> RegistrarCuenta([FromBody] CredsUsuarioRegister credsUsuarioRegiser)
        {
            try
            {
                // ---------------------
                // creando username 
                credsUsuarioRegiser.Names = credsUsuarioRegiser.Names.Split(' ')[0] + "-" + credsUsuarioRegiser.Names.Split(' ')[1];
                // creando un objeto de tipo IdentityUser
                var usuarioRegistrar = new IdentityUser { UserName = credsUsuarioRegiser.Names, Email = credsUsuarioRegiser.Email };
                // agregando a la bd
                var result = await userManager.CreateAsync(usuarioRegistrar);
                // verificar que todo este correcto
                if (result.Succeeded) return await ConstruirToken(credsUsuarioRegiser);
                return BadRequest("El usuario ya se encuentra registrado");
            }
            catch (Exception ex)
            {
                // Recurso no encontrado. Se utiliza cuando el servidor web no encuentra la página o recurso solicitado.
                return BadRequest($"Ocurrió un error: {ex}");
            }
        }
        [HttpGet("login")]
        public async Task<ActionResult<RespuestaAutenticacion>> Login([FromQuery] string email)
        {
            try
            {
                // ---------------------
                var res = await userManager.FindByEmailAsync(email);
                if (res == null) return BadRequest("No existe el usuario");
                return await ConstruirToken(new CredsUsuarioRegister { Email=email, Names = res.UserName});
            }
            catch (Exception ex)
            {
                // Recurso no encontrado. Se utiliza cuando el servidor web no encuentra la página o recurso solicitado.
                return BadRequest($"Ocurrió un error: {ex}");
            }
        }
        private async Task<RespuestaAutenticacion> ConstruirToken(CredsUsuarioRegister usuario)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", usuario.Email),
            };
            var _usuario = await userManager.FindByEmailAsync(usuario.Email);
            var claimsDB = await userManager.GetClaimsAsync(_usuario);
            claims.AddRange(claimsDB);
            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddMinutes(3);
            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion, signingCredentials: creds);
            return new RespuestaAutenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiracion = expiracion,
                Names = usuario.Names
            };
        }
    } 
}
