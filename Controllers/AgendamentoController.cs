using Microsoft.AspNetCore.Mvc;
using TechSphere.Models;
using TechSphere.Models.Agendamentos;
using TechSphere.Service.Interface;

namespace TechSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamentoService _service;

        public AgendamentoController(IAgendamentoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _service.GetAgendamentosAsync();
            return Ok(new { message = "Agendamentos encontrados.", data = lista });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var agendamento = await _service.GetAgendamentoAsync(id);
            if (agendamento == null)
                return NotFound(new { message = "Agendamento não encontrado." });

            return Ok(new { message = "Agendamento encontrado.", data = agendamento });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Agendamento agendamento)
        {
            var criado = await _service.AdicionarAgendamentoAsync(agendamento);
            return CreatedAtAction(nameof(GetById), new { id = criado.IdAgendamento }, new
            {
                message = "Agendamento criado com sucesso.",
                data = criado
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Agendamento agendamento)
        {
            if (agendamento.IdAgendamento == 0)
                return BadRequest(new { message = "ID inválido para atualização." });

            await _service.AtualizarAgendamentoAsync(agendamento);
            return Ok(new { message = "Agendamento atualizado com sucesso." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.RemoverAgendamentoAsync(id);
            return Ok(new { message = "Agendamento removido com sucesso." });
        }
    }
}
