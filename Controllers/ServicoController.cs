using Microsoft.AspNetCore.Mvc;
using TechSphere.Models.Agendamentos;
using TechSphere.Service.Interface;

namespace TechSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicoController : ControllerBase
    {
        private readonly IServicoService _service;

        public ServicoController(IServicoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _service.GetServicosAsync();
            return Ok(new { message = "Serviços encontrados.", data = lista });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var servico = await _service.GetServicoAsync(id);
            if (servico == null)
                return NotFound(new { message = "Serviço não encontrado." });

            return Ok(new { message = "Serviço encontrado.", data = servico });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Servico servico)
        {
            var criado = await _service.AdicionarServicoAsync(servico);
            return CreatedAtAction(nameof(GetById), new { id = criado.IdServico }, new
            {
                message = "Serviço criado com sucesso.",
                data = criado
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Servico servico)
        {
            if (servico.IdServico == 0)
                return BadRequest(new { message = "ID inválido para atualização." });

            await _service.AtualizarServicoAsync(servico);
            return Ok(new { message = "Serviço atualizado com sucesso." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.RemoverServicoAsync(id);
            return Ok(new { message = "Serviço removido com sucesso." });
        }
    }
}
