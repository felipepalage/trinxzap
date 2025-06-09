using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TechSphere.Models.Agendamentos;
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
        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<HorarioProfissional> HorariosProfissional { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Configuracao> Configuracoes { get; set; }

        public DbSet<Agendamento> Agendamentos { get; set; }

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

            modelBuilder.Entity<HorarioProfissional>(entity =>
            {
                entity.ToTable("TB_HORARIO_PROFISSIONAL");

                entity.HasKey(e => e.IdHorario);

                entity.Property(e => e.IdHorario).HasColumnName("ID_HORARIO");
                entity.Property(e => e.IdProfissional).HasColumnName("ID_PROFISSIONAL");
                entity.Property(e => e.DiaSemana).HasColumnName("DIA_SEMANA");
                entity.Property(e => e.HrInicio).HasColumnName("HR_INICIO");
                entity.Property(e => e.HrFim).HasColumnName("HR_FIM");
            });

            modelBuilder.Entity<Configuracao>(entity =>
            {
                entity.ToTable("TB_CONFIGURACAO");

                entity.HasKey(e => e.IdConfig);

                entity.Property(e => e.IdConfig).HasColumnName("ID_CONFIG");
                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
                entity.Property(e => e.Chave).HasColumnName("CHAVE");
                entity.Property(e => e.Valor).HasColumnName("VALOR");
            });

            modelBuilder.Entity<Servico>(entity =>
            {
                entity.ToTable("TB_SERVICO");

                entity.HasKey(e => e.IdServico);

                entity.Property(e => e.IdServico).HasColumnName("ID_SERVICO");
                entity.Property(e => e.NomeServico).HasColumnName("NM_SERVICO");
                entity.Property(e => e.ValorPreco).HasColumnName("VL_PRECO");
                entity.Property(e => e.Descricao).HasColumnName("DS_DESCRICAO");
                entity.Property(e => e.Ativo).HasColumnName("IC_ATIVO");
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


            modelBuilder.Entity<Profissional>(entity =>
            {
                entity.ToTable("TB_PROFISSIONAL");

                entity.HasKey(e => e.IdProfissional);
                entity.Property(e => e.IdProfissional).HasColumnName("ID_PROFISSIONAL");
                entity.Property(e => e.NomeProfissional).HasColumnName("NM_PROFISSIONAL");
                entity.Property(e => e.Email).HasColumnName("EMAIL");
                entity.Property(e => e.Especialidade).HasColumnName("ESPECIALIDADE");
                entity.Property(e => e.Ativo).HasColumnName("IC_ATIVO");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("TB_USUARIO");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID_USUARIO");
                entity.Property(e => e.Nome).HasColumnName("NM_USUARIO");
                entity.Property(e => e.Email).HasColumnName("EMAIL");
                entity.Property(e => e.SenhaHash).HasColumnName("SENHA_HASH");
            });

            modelBuilder.Entity<Agendamento>(entity =>
            {
                entity.Property(e => e.IdAgendamento).HasColumnName("ID_AGENDAMENTO");
                entity.Property(e => e.NomeCliente).HasColumnName("NM_CLIENTE");
                entity.Property(e => e.NomeServico).HasColumnName("NM_SERVICO");
                entity.Property(e => e.DataAgendamento).HasColumnName("DT_AGENDAMENTO");
                entity.Property(e => e.Status).HasColumnName("DS_STATUS");
                entity.Property(e => e.IdProfissional).HasColumnName("ID_PROFISSIONAL");
            });
        }
    }
}
