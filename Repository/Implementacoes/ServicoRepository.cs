using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TechSphere.Models.Agendamentos;
using TechSphere.Repository.Interface;
using TrinxZap.API.Data;

namespace TechSphere.Repository.Implementacoes
{
    public class ServicoRepository : IServicoRepository
    {
        private readonly AppDbContext _context;

        public ServicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Servico>> GetAllAsync()
        {
            return await _context.Servicos
                .FromSqlRaw("SELECT * FROM TB_SERVICO")
                .ToListAsync();
        }

        public async Task<Servico?> GetByIdAsync(int id)
        {
            return await _context.Servicos
                .FromSqlRaw("SELECT * FROM TB_SERVICO WHERE ID_SERVICO = @ID",
                    new SqlParameter("@ID", id))
                .FirstOrDefaultAsync();
        }

        public async Task<Servico> CreateAsync(Servico servico)
        {
            var query = @"
                INSERT INTO TB_SERVICO (NM_SERVICO, VL_PRECO, DS_DESCRICAO, IC_ATIVO)
                VALUES (@NM_SERVICO, @VL_PRECO, @DS_DESCRICAO, @IC_ATIVO)";

            var parametros = new[]
            {
                new SqlParameter("@NM_SERVICO", servico.NomeServico),
                new SqlParameter("@VL_PRECO", servico.ValorPreco),
                new SqlParameter("@DS_DESCRICAO", servico.Descricao),
                new SqlParameter("@IC_ATIVO", servico.Ativo)
            };

            await _context.Database.ExecuteSqlRawAsync(query, parametros);

            return await _context.Servicos
                .FromSqlRaw("SELECT TOP 1 * FROM TB_SERVICO ORDER BY ID_SERVICO DESC")
                .FirstAsync();
        }

        public async Task UpdateAsync(Servico servico)
        {
            var query = @"
                UPDATE TB_SERVICO
                SET NM_SERVICO = @NM_SERVICO,
                    VL_PRECO = @VL_PRECO,
                    DS_DESCRICAO = @DS_DESCRICAO,
                    IC_ATIVO = @IC_ATIVO
                WHERE ID_SERVICO = @ID";

            var parametros = new[]
            {
                new SqlParameter("@NM_SERVICO", servico.NomeServico),
                new SqlParameter("@VL_PRECO", servico.ValorPreco),
                new SqlParameter("@DS_DESCRICAO", servico.Descricao),
                new SqlParameter("@IC_ATIVO", servico.Ativo),
                new SqlParameter("@ID", servico.IdServico)
            };

            await _context.Database.ExecuteSqlRawAsync(query, parametros);
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "DELETE FROM TB_SERVICO WHERE ID_SERVICO = @ID",
                new SqlParameter("@ID", id));
        }
    }
}
