using Microsoft.AspNetCore.Mvc;
using TechSphere.Models;
using TechSphere.Models.Agendamentos;
using TechSphere.Service.Interface;

namespace TechSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorarioProfissionalController : ControllerBase
    {
        private readonly IHorarioProfissionalService _service;

        public HorarioProfissionalController(IHorarioProfissionalService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _service.GetHorariosAsync();
            return Ok(new { message = "Horários encontrados.", data = lista });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetHorarioAsync(id);
            if (item == null)
                return NotFound(new { message = "Horário não encontrado." });

            return Ok(new { message = "Horário encontrado.", data = item });
        }

        [HttpPost]
        public async Task<IActionResult> Create(HorarioProfissional horario)
        {
            var criado = await _service.AdicionarHorarioAsync(horario);
            return CreatedAtAction(nameof(GetById), new { id = criado.IdHorario }, new
            {
                message = "Horário criado com sucesso.",
                data = criado
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] HorarioProfissional horario)
        {
            if (horario.IdHorario == 0)
                return BadRequest(new { message = "ID inválido para atualização." });

            await _service.AtualizarHorarioAsync(horario);
            return Ok(new { message = "Horário atualizado com sucesso." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.RemoverHorarioAsync(id);
            return Ok(new { message = "Horário removido com sucesso." });
        }
    }
}
