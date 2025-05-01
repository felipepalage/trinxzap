using trinxzap.Models;

namespace trinxzap.Service.Interface;

  public interface IPedidoService
    {
        Task<IEnumerable<Pedido>> GetPedidosAsync();
        Task<Pedido?> GetPedidoAsync(int id);
        Task<Pedido> AdicionarPedidoAsync(Pedido pedido);
        Task AtualizarPedidoAsync(Pedido pedido);
        Task RemoverPedidoAsync(int id);
    }

