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
                var user = await userManager.FindByEmailAsync(credsUsuarioRegiser.Email);
                if (user!=null&&user.Email == credsUsuarioRegiser.Email) return BadRequest("Ya existe");
                // creando username 
                //credsUsuarioRegiser.Names = credsUsuarioRegiser.Names.Split(' ')[0] + "-" + credsUsuarioRegiser.Names.Split(' ')[1];
                credsUsuarioRegiser.Names=ConvertirNombre(credsUsuarioRegiser.Names);
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
        [HttpPut("actualizar")]
        public async Task<ActionResult> Actualizar([FromQuery] string newName,[FromQuery] string name)
        {
            try
            {
                newName=ConvertirNombre(newName);
                var user = await userManager.FindByNameAsync(name.Trim());
                var result = await userManager.SetUserNameAsync(user, newName);
                return Ok();
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
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
        private static string ConvertirNombre(string nombre)
        {
            string tem = "";
            nombre = Regex.Replace(nombre.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
            string[] valores = nombre.ToUpper().Trim().Split(' ');
            foreach (var i in valores)
                tem +=i==valores[valores.Length-1]? i:i + "-";
            return tem;
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
