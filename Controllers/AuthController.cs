using Microsoft.AspNetCore.Mvc;
using TechSphere.Models.Agendamentos;
using TechSphere.Service.Interface;

namespace TechSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistroUsuario dto)
        {
            if (await _authService.UsuarioExisteAsync(dto.Email))
                return BadRequest(new { message = "E-mail já cadastrado." });

            var usuario = await _authService.RegistrarUsuarioAsync(dto);

            return Ok(new
            {
                message = "Usuário registrado com sucesso.",
                data = usuario
            });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuario dto)
        {
            var usuario = await _authService.LoginAsync(dto);
            if (usuario == null)
                return Unauthorized(new { message = "Credenciais inválidas." });

            return Ok(new
            {
                message = "Login realizado com sucesso.",
                data = usuario
            });
        }


        [HttpGet("exists")]
        public async Task<IActionResult> CheckIfExists([FromQuery] string email)
        {
            var exists = await _authService.UsuarioExisteAsync(email);

            return Ok(new
            {
                message = exists ? "E-mail já cadastrado." : "E-mail disponível.",
                data = new { exists }
            });
        }

    }
}
