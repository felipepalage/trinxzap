using Microsoft.AspNetCore.Mvc;
using TechSphere.Models.Agendamentos;
using TechSphere.Service.Interface;

namespace TechSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfissionalController : ControllerBase
    {
        private readonly IProfissionalService _service;

        public ProfissionalController(IProfissionalService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _service.GetProfissionaisAsync();

            return Ok(new
            {
                message = "Lista de profissionais obtida com sucesso.",
                data = lista
            });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetProfissionalAsync(id);
            if (item == null)
                return NotFound(new { message = "Profissional não encontrado." });

            return Ok(new
            {
                message = "Profissional encontrado com sucesso.",
                data = item
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Profissional profissional)
        {
            var criado = await _service.AdicionarProfissionalAsync(profissional);

            return CreatedAtAction(nameof(GetById), new { id = criado.IdProfissional }, new
            {
                message = "Profissional criado com sucesso.",
                data = criado
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Profissional profissional)
        {
            if (profissional.IdProfissional == 0)
                return BadRequest(new { message = "ID inválido." });

            await _service.AtualizarProfissionalAsync(profissional);

            return Ok(new { message = "Profissional atualizado com sucesso." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.RemoverProfissionalAsync(id);

            return Ok(new { message = "Profissional removido com sucesso." });
        }

    }
}
