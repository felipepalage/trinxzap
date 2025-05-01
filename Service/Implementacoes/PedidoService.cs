using System.Collections.Generic;
using System.Threading.Tasks;
using trinxzap.Models;
using trinxzap.Service.Interface;
using TrinxZap.Repository.Interface;

namespace TrinxZap.API.Services.Implementacoes
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;

        public PedidoService(IPedidoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Pedido>> GetPedidosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Pedido?> GetPedidoAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Pedido> AdicionarPedidoAsync(Pedido pedido)
        {
            return await _repository.CreateAsync(pedido);
        }

        public async Task AtualizarPedidoAsync(Pedido pedido)
        {
            await _repository.UpdateAsync(pedido);
        }

        public async Task RemoverPedidoAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
