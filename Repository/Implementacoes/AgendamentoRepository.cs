using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TechSphere.Models;
using TechSphere.Models.Agendamentos;
using TechSphere.Repository.Interface;
using TrinxZap.API.Data;

namespace TechSphere.Repository.Implementacoes
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly AppDbContext _context;

        public AgendamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Agendamento>> GetAllAsync()
        {
            return await _context.Agendamentos
                .FromSqlRaw("SELECT * FROM TB_AGENDAMENTO")
                .ToListAsync();
        }

        public async Task<Agendamento?> GetByIdAsync(int id)
        {
            return await _context.Agendamentos
                .FromSqlRaw("SELECT * FROM TB_AGENDAMENTO WHERE ID_AGENDAMENTO = @ID",
                    new SqlParameter("@ID", id))
                .FirstOrDefaultAsync();
        }

        public async Task<Agendamento> CreateAsync(Agendamento agendamento)
        {
            var query = @"
                INSERT INTO TB_AGENDAMENTO (NM_CLIENTE, NM_SERVICO, DT_AGENDAMENTO, DS_STATUS, ID_PROFISSIONAL)
                VALUES (@NM_CLIENTE, @NM_SERVICO, @DT_AGENDAMENTO, @DS_STATUS, @ID_PROFISSIONAL)";

            var parametros = new[]
            {
                new SqlParameter("@NM_CLIENTE", agendamento.NomeCliente),
                new SqlParameter("@NM_SERVICO", agendamento.NomeServico),
                new SqlParameter("@DT_AGENDAMENTO", agendamento.DataAgendamento),
                new SqlParameter("@DS_STATUS", agendamento.Status),
                new SqlParameter("@ID_PROFISSIONAL", agendamento.IdProfissional)
            };

            await _context.Database.ExecuteSqlRawAsync(query, parametros);

            return await _context.Agendamentos
                .FromSqlRaw("SELECT TOP 1 * FROM TB_AGENDAMENTO ORDER BY ID_AGENDAMENTO DESC")
                .FirstAsync();
        }

        public async Task UpdateAsync(Agendamento agendamento)
        {
            var query = @"
                UPDATE TB_AGENDAMENTO
                SET NM_CLIENTE = @NM_CLIENTE,
                    NM_SERVICO = @NM_SERVICO,
                    DT_AGENDAMENTO = @DT_AGENDAMENTO,
                    DS_STATUS = @DS_STATUS,
                    ID_PROFISSIONAL = @ID_PROFISSIONAL
                WHERE ID_AGENDAMENTO = @ID";

            var parametros = new[]
            {
                new SqlParameter("@NM_CLIENTE", agendamento.NomeCliente),
                new SqlParameter("@NM_SERVICO", agendamento.NomeServico),
                new SqlParameter("@DT_AGENDAMENTO", agendamento.DataAgendamento),
                new SqlParameter("@DS_STATUS", agendamento.Status),
                new SqlParameter("@ID_PROFISSIONAL", agendamento.IdProfissional),
                new SqlParameter("@ID", agendamento.IdAgendamento)
            };

            await _context.Database.ExecuteSqlRawAsync(query, parametros);
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "DELETE FROM TB_AGENDAMENTO WHERE ID_AGENDAMENTO = @ID",
                new SqlParameter("@ID", id));
        }
    }
}
