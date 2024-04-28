using MediConecctPro.Utils;
using MediConnectPro.Bs.Servicios;
using MediConnectPro.Core.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MediConecctPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUsuariosServicio _usuariosServicio;
        private readonly UtilsCls utls = new UtilsCls();
        public AuthController(IUsuariosServicio usuariosServicio, IConfiguration config)
        {
            _usuariosServicio = usuariosServicio;
            _config = config;
        }

        [HttpPost, AllowAnonymous]
        [Route("Login")]
        public async Task<IEnumerable<UsuariosDto>> Login(Usuarios usuarios)
        {

            if (string.IsNullOrEmpty(usuarios.Correo))
            {
                return new List<UsuariosDto>();
            }
            if (string.IsNullOrEmpty(usuarios.Contrasena))
            {
                return new List<UsuariosDto>();
            }
            usuarios.Contrasena = utls.Encript(usuarios.Contrasena);
            IEnumerable<UsuariosDto> usr = await _usuariosServicio.TraerUsuarios(usuarios);
            if (usr.Any())
            {
                var lst = usr.FirstOrDefault();
                var tk = GenerateToken(new() { Documento = lst!.Documento!.Trim(), Correo = lst!.Correo!.Trim(), Id = lst!.Id!.Value.ToString() }, lst);
                usr.FirstOrDefault()!.Token = tk.Token;
                usr.FirstOrDefault()!.ExpirationToken = tk.ExpirationToken;
            }
            return usr;
        }

        private UsuariosDto GenerateToken(UserClaims userClaims, UsuariosDto? usuarios)
        {
            if (userClaims == null)
                return usuarios;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Authentication, userClaims.Id!),
                new Claim(ClaimTypes.Actor, userClaims.Documento!),
                new Claim(ClaimTypes.Email, userClaims.Correo!),
            };

            DateTime expira = DateTime.Now.AddHours(1);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: expira,
              signingCredentials: credentials);

            usuarios.Token = new JwtSecurityTokenHandler().WriteToken(token);
            usuarios.ExpirationToken = expira;

            return usuarios;
        }
    }
}
