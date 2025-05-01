using trinxzap.Models;

public class Pedido
{
    public int PedidoId { get; set; }
    public int MesaId { get; set; }
    public Mesa Mesa { get; set; }  // <- ADICIONE ISSO
    public string Status { get; set; } = "Pendente";
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public string? Observacao { get; set; }
    public List<ItemPedido> Itens { get; set; } = new();
}
