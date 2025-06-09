using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TechSphere.Models.Agendamentos;
using TechSphere.Repository.Interface;
using TrinxZap.API.Data;

namespace TechSphere.Repository.Implementacoes
{
    public class ProfissionalRepository : IProfissionalRepository
    {
        private readonly AppDbContext _context;

        public ProfissionalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Profissional>> GetAllAsync()
        {
            return await _context.Profissionais
                .FromSqlRaw("SELECT * FROM TB_PROFISSIONAL")
                .ToListAsync();
        }

        public async Task<Profissional?> GetByIdAsync(int id)
        {
            return await _context.Profissionais
                .FromSqlRaw("SELECT * FROM TB_PROFISSIONAL WHERE ID_PROFISSIONAL = @ID",
                    new SqlParameter("@ID", id))
                .FirstOrDefaultAsync();
        }

        public async Task<Profissional> CreateAsync(Profissional profissional)
        {
            var query = @"
                INSERT INTO TB_PROFISSIONAL (NM_PROFISSIONAL, EMAIL, ESPECIALIDADE, IC_ATIVO)
                VALUES (@NM_PROFISSIONAL, @EMAIL, @ESPECIALIDADE, @IC_ATIVO)";

            var parametros = new[]
            {
                new SqlParameter("@NM_PROFISSIONAL", profissional.NomeProfissional),
                new SqlParameter("@EMAIL", profissional.Email),
                new SqlParameter("@ESPECIALIDADE", profissional.Especialidade),
                new SqlParameter("@IC_ATIVO", profissional.Ativo)
            };

            await _context.Database.ExecuteSqlRawAsync(query, parametros);

            return await _context.Profissionais
                .FromSqlRaw("SELECT TOP 1 * FROM TB_PROFISSIONAL ORDER BY ID_PROFISSIONAL DESC")
                .FirstAsync();
        }

        public async Task UpdateAsync(Profissional profissional)
        {
            var query = @"
                UPDATE TB_PROFISSIONAL
                SET NM_PROFISSIONAL = @NM_PROFISSIONAL,
                    EMAIL = @EMAIL,
                    ESPECIALIDADE = @ESPECIALIDADE,
                    IC_ATIVO = @IC_ATIVO
                WHERE ID_PROFISSIONAL = @ID";

            var parametros = new[]
            {
                new SqlParameter("@NM_PROFISSIONAL", profissional.NomeProfissional),
                new SqlParameter("@EMAIL", profissional.Email),
                new SqlParameter("@ESPECIALIDADE", profissional.Especialidade),
                new SqlParameter("@IC_ATIVO", profissional.Ativo),
                new SqlParameter("@ID", profissional.IdProfissional)
            };

            await _context.Database.ExecuteSqlRawAsync(query, parametros);
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM TB_PROFISSIONAL WHERE ID_PROFISSIONAL = @ID";
            await _context.Database.ExecuteSqlRawAsync(query, new SqlParameter("@ID", id));
        }
    }
}

