namespace trinxzap.Repository.Implementacoes;
using trinxzap.Models;
using System;
using TrinxZap.Repository.Interface;
using TrinxZap.API.Data;
using Microsoft.EntityFrameworkCore;

public class PedidoRepository : IPedidoRepository
{
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

    public async Task<IEnumerable<Pedido>> GetAllAsync()
    {
        var pedidos = await _context.Pedidos
            .FromSqlRaw("SELECT * FROM TB_PEDIDOS")
            .ToListAsync();

        foreach (var pedido in pedidos)
        {
            pedido.Itens = await _context.ItensPedido
                .FromSqlRaw("SELECT * FROM TB_ITENS_PEDIDO WHERE ID_PEDIDO = {0}", pedido.PedidoId)
                .ToListAsync();
        }

        return pedidos;
    }

    public async Task<Pedido?> GetByIdAsync(int id)
    {
        var pedido = await _context.Pedidos
            .FromSqlRaw("SELECT * FROM TB_PEDIDOS WHERE ID_PEDIDO = {0}", id)
            .FirstOrDefaultAsync();

        if (pedido != null)
        {
            pedido.Itens = await _context.ItensPedido
                .FromSqlRaw("SELECT * FROM TB_ITENS_PEDIDO WHERE ID_PEDIDO = {0}", id)
                .ToListAsync();
        }

        return pedido;
    }

    public async Task<Pedido> CreateAsync(Pedido pedido)
    {
        await _context.Database.ExecuteSqlRawAsync(@"
            INSERT INTO TB_PEDIDOS (ID_MESA, NM_STATUS, DT_CRIACAO, DS_OBSERVACAO)
            VALUES ({0}, {1}, {2}, {3})
        ", pedido.MesaId, pedido.Status ?? "Pendente", pedido.DataCriacao, pedido.Observacao);

        var novoPedido = await _context.Pedidos
            .FromSqlRaw("SELECT TOP 1 * FROM TB_PEDIDOS ORDER BY ID_PEDIDO DESC")
            .FirstAsync();

        foreach (var item in pedido.Itens)
        {
            await _context.Database.ExecuteSqlRawAsync(@"
                INSERT INTO TB_ITENS_PEDIDO (ID_PEDIDO, ID_PRODUTO, NR_QUANTIDADE, VL_UNITARIO)
                VALUES ({0}, {1}, {2}, {3})
            ", novoPedido.PedidoId, item.ProdutoId, item.Quantidade, item.PrecoUnitario);
        }

        return novoPedido;
    }

    public async Task UpdateAsync(Pedido pedido)
    {
        await _context.Database.ExecuteSqlRawAsync(@"
            UPDATE TB_PEDIDOS
            SET ID_MESA = {0}, NM_STATUS = {1}, DS_OBSERVACAO = {2}
            WHERE ID_PEDIDO = {3}
        ", pedido.MesaId, pedido.Status, pedido.Observacao, pedido.PedidoId);
    }

    public async Task DeleteAsync(int id)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "DELETE FROM TB_ITENS_PEDIDO WHERE ID_PEDIDO = {0}", id);

        await _context.Database.ExecuteSqlRawAsync(
            "DELETE FROM TB_PEDIDOS WHERE ID_PEDIDO = {0}", id);
    }
}



