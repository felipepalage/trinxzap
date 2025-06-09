using Microsoft.AspNetCore.Mvc;
using TechSphere.Models;
using TechSphere.Models.Agendamentos;
using TechSphere.Service.Interface;

namespace TechSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfiguracaoController : ControllerBase
    {
        private readonly IConfiguracaoService _service;

        public ConfiguracaoController(IConfiguracaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _service.GetConfiguracoesAsync();
            return Ok(new { message = "Configurações encontradas.", data = lista });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetConfiguracaoAsync(id);
            if (item == null)
                return NotFound(new { message = "Configuração não encontrada." });

            return Ok(new { message = "Configuração encontrada.", data = item });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Configuracao config)
        {
            var criado = await _service.AdicionarConfiguracaoAsync(config);
            return CreatedAtAction(nameof(GetById), new { id = criado.IdConfig }, new
            {
                message = "Configuração criada com sucesso.",
                data = criado
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Configuracao config)
        {
            if (config.IdConfig == 0)
                return BadRequest(new { message = "ID inválido para atualização." });

            await _service.AtualizarConfiguracaoAsync(config);
            return Ok(new { message = "Configuração atualizada com sucesso." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.RemoverConfiguracaoAsync(id);
            return Ok(new { message = "Configuração removida com sucesso." });
        }
    }
}
