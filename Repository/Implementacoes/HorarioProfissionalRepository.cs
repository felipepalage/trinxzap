using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TechSphere.Models;
using TechSphere.Models.Agendamentos;
using TechSphere.Repository.Interface;
using TrinxZap.API.Data;

namespace TechSphere.Repository.Implementacoes
{
    public class HorarioProfissionalRepository : IHorarioProfissionalRepository
    {
        private readonly AppDbContext _context;

        public HorarioProfissionalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HorarioProfissional>> GetAllAsync()
        {
            return await _context.HorariosProfissional
                .FromSqlRaw("SELECT * FROM TB_HORARIO_PROFISSIONAL")
                .ToListAsync();
        }

        public async Task<HorarioProfissional?> GetByIdAsync(int id)
        {
            return await _context.HorariosProfissional
                .FromSqlRaw("SELECT * FROM TB_HORARIO_PROFISSIONAL WHERE ID_HORARIO = @ID",
                    new SqlParameter("@ID", id))
                .FirstOrDefaultAsync();
        }

        public async Task<HorarioProfissional> CreateAsync(HorarioProfissional horario)
        {
            var query = @"
                INSERT INTO TB_HORARIO_PROFISSIONAL (ID_PROFISSIONAL, DIA_SEMANA, HR_INICIO, HR_FIM)
                VALUES (@ID_PROFISSIONAL, @DIA_SEMANA, @HR_INICIO, @HR_FIM)";

            var parametros = new[]
            {
                new SqlParameter("@ID_PROFISSIONAL", horario.IdProfissional),
                new SqlParameter("@DIA_SEMANA", horario.DiaSemana),
                new SqlParameter("@HR_INICIO", horario.HrInicio),
                new SqlParameter("@HR_FIM", horario.HrFim)
            };

            await _context.Database.ExecuteSqlRawAsync(query, parametros);

            return await _context.HorariosProfissional
                .FromSqlRaw("SELECT TOP 1 * FROM TB_HORARIO_PROFISSIONAL ORDER BY ID_HORARIO DESC")
                .FirstAsync();
        }

        public async Task UpdateAsync(HorarioProfissional horario)
        {
            var query = @"
                UPDATE TB_HORARIO_PROFISSIONAL
                SET ID_PROFISSIONAL = @ID_PROFISSIONAL,
                    DIA_SEMANA = @DIA_SEMANA,
                    HR_INICIO = @HR_INICIO,
                    HR_FIM = @HR_FIM
                WHERE ID_HORARIO = @ID";

            var parametros = new[]
            {
                new SqlParameter("@ID_PROFISSIONAL", horario.IdProfissional),
                new SqlParameter("@DIA_SEMANA", horario.DiaSemana),
                new SqlParameter("@HR_INICIO", horario.HrInicio),
                new SqlParameter("@HR_FIM", horario.HrFim),
                new SqlParameter("@ID", horario.IdHorario)
            };

            await _context.Database.ExecuteSqlRawAsync(query, parametros);
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "DELETE FROM TB_HORARIO_PROFISSIONAL WHERE ID_HORARIO = @ID",
                new SqlParameter("@ID", id));
        }
    }
}
