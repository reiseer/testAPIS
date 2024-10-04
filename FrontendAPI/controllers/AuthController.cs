using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FrontendAPI.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")] 
        public IActionResult Login([FromBody] LoginModel model)
        {
            // Aquí deberías validar las credenciales del usuario
            // Por simplicidad, asumimos que el usuario es válido si proporciona un token
            if (model.Username == "admin" && model.Password == "admin")
            {
                var token = GenerateToken(model.Username);
                return Ok(new { token });
            }
            else
            {
                return Unauthorized("Credenciales inválidas");
            }   
        }

        private string GenerateToken(string username)
        {
            var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("La clave JWT no está configurada");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: null,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginModel
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
