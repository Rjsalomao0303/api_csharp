using CadastroClientes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CadastroClientes.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string _secretKey;

        public AuthController()
        {
            _secretKey = "Use@l2025-04-03#RjsalomaoDev"; // A chave deve ser forte e segura
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario usuario)
        {
            // Simulação de um usuário fixo (posteriormente, pode vir do banco de dados)
            if (usuario.Username != "admin" || usuario.Password != "123")
                return Unauthorized("Usuário ou senha inválidos");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, usuario.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(2), // Token válido por 2 horas
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { token = tokenString });
        }
    }
}
