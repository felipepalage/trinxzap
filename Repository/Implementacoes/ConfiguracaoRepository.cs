using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TechSphere.Models;
using TechSphere.Models.Agendamentos;
using TechSphere.Repository.Interface;
using TrinxZap.API.Data;

namespace TechSphere.Repository.Implementacoes
{
    public class ConfiguracaoRepository : IConfiguracaoRepository
    {
        private readonly AppDbContext _context;

        public ConfiguracaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Configuracao>> GetAllAsync()
        {
            return await _context.Configuracoes
                .FromSqlRaw("SELECT * FROM TB_CONFIGURACAO")
                .ToListAsync();
        }

        public async Task<Configuracao?> GetByIdAsync(int id)
        {
            return await _context.Configuracoes
                .FromSqlRaw("SELECT * FROM TB_CONFIGURACAO WHERE ID_CONFIG = @ID",
                    new SqlParameter("@ID", id))
                .FirstOrDefaultAsync();
        }

        public async Task<Configuracao> CreateAsync(Configuracao config)
        {
            var query = @"
                INSERT INTO TB_CONFIGURACAO (ID_USUARIO, CHAVE, VALOR)
                VALUES (@ID_USUARIO, @CHAVE, @VALOR)";

            var parametros = new[]
            {
                new SqlParameter("@ID_USUARIO", config.IdUsuario),
                new SqlParameter("@CHAVE", config.Chave),
                new SqlParameter("@VALOR", config.Valor)
            };

            await _context.Database.ExecuteSqlRawAsync(query, parametros);

            return await _context.Configuracoes
                .FromSqlRaw("SELECT TOP 1 * FROM TB_CONFIGURACAO ORDER BY ID_CONFIG DESC")
                .FirstAsync();
        }

        public async Task UpdateAsync(Configuracao config)
        {
            var query = @"
                UPDATE TB_CONFIGURACAO
                SET ID_USUARIO = @ID_USUARIO,
                    CHAVE = @CHAVE,
                    VALOR = @VALOR
                WHERE ID_CONFIG = @ID";

            var parametros = new[]
            {
                new SqlParameter("@ID_USUARIO", config.IdUsuario),
                new SqlParameter("@CHAVE", config.Chave),
                new SqlParameter("@VALOR", config.Valor),
                new SqlParameter("@ID", config.IdConfig)
            };

            await _context.Database.ExecuteSqlRawAsync(query, parametros);
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "DELETE FROM TB_CONFIGURACAO WHERE ID_CONFIG = @ID",
                new SqlParameter("@ID", id));
        }
    }
}
