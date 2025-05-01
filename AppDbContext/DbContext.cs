using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using trinxzap.Models;

namespace TrinxZap.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.ToTable("TB_PEDIDOS");

                entity.HasKey(e => e.PedidoId);

                entity.Property(e => e.PedidoId).HasColumnName("ID_PEDIDO");
                entity.Property(e => e.MesaId).HasColumnName("ID_MESA");
                entity.Property(e => e.Status).HasColumnName("DS_STATUS");
                entity.Property(e => e.DataCriacao).HasColumnName("DT_CRIACAO");
                entity.Property(e => e.Observacao).HasColumnName("DS_OBSERVACAO");
            });


            modelBuilder.Entity<ItemPedido>(entity =>
            {
                entity.ToTable("TB_ITENS_PEDIDO");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID_ITEM");
                entity.Property(e => e.PedidoId).HasColumnName("ID_PEDIDO");
                entity.Property(e => e.ProdutoId).HasColumnName("ID_PRODUTO");
                entity.Property(e => e.Quantidade).HasColumnName("NR_QUANTIDADE");
                entity.Property(e => e.PrecoUnitario).HasColumnName("VL_UNITARIO");
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.ToTable("TB_PRODUTOS");
                entity.HasKey(e => e.ProdutoId);
                entity.Property(e => e.ProdutoId).HasColumnName("ID_PRODUTO");
                entity.Property(e => e.Nome).HasColumnName("NM_PRODUTO");
                entity.Property(e => e.Preco).HasColumnName("VL_PRECO");
                entity.Property(e => e.Categoria).HasColumnName("NM_CATEGORIA");
            });

            modelBuilder.Entity<Mesa>(entity =>
            {
                entity.ToTable("TB_MESAS");
                entity.HasKey(e => e.MesaId);
                entity.Property(e => e.MesaId).HasColumnName("ID_MESA");
                entity.Property(e => e.Numero).HasColumnName("NR_MESA");
                entity.Property(e => e.Ocupada).HasColumnName("IC_OCUPADA");
            });
        }



    }
}
