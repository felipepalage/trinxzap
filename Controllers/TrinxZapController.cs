using Microsoft.AspNetCore.Mvc;
using trinxzap.Models;
using trinxzap.Service.Interface;
using TrinxZap.API.Services;

namespace TrinxZap.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _service;

        public PedidoController(IPedidoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pedidos = await _service.GetPedidosAsync();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pedido = await _service.GetPedidoAsync(id);
            if (pedido == null) return NotFound();
            return Ok(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pedido pedido)
        {
            var created = await _service.AdicionarPedidoAsync(pedido);
            return CreatedAtAction(nameof(GetById), new { id = created.PedidoId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Pedido pedido)
        {
            if (id != pedido.PedidoId) return BadRequest();
            await _service.AtualizarPedidoAsync(pedido);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.RemoverPedidoAsync(id);
            return NoContent();
        }
    }
}