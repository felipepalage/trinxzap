using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TechSphere.Models.Agendamentos;
using TechSphere.Repository.Interface;
using TrinxZap.API.Data;


namespace TechSphere.Repository.Implementacoes
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UsuarioExiste(string email)
        {
            var emailParam = new SqlParameter("@EMAIL", email);

            var resultado = await _context.Usuarios
                .FromSqlRaw("SELECT * FROM TB_USUARIO WHERE EMAIL = @EMAIL", emailParam)
                .AnyAsync();

            return resultado;
        }

        public async Task<Usuario> Registrar(RegistroUsuario dto)
        {
            using var sha256 = SHA256.Create();
            var senhaHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(dto.Senha)));

            var query = @"
    INSERT INTO TB_USUARIO (NM_USUARIO, EMAIL, SENHA_HASH)
    VALUES (@NM_USUARIO, @EMAIL, @SENHA_HASH)";

            var parametros = new[]
            {
        new SqlParameter("@NM_USUARIO", dto.Nome),
        new SqlParameter("@EMAIL", dto.Email),
        new SqlParameter("@SENHA_HASH", senhaHash)
    };

            await _context.Database.ExecuteSqlRawAsync(query, parametros);

            var user = await _context.Usuarios
                .FromSqlRaw("SELECT TOP 1 * FROM TB_USUARIO WHERE EMAIL = @EMAIL ORDER BY ID_USUARIO DESC",
                    new SqlParameter("@EMAIL", dto.Email))
                .FirstOrDefaultAsync();

            return user!;
        }


        public async Task<Usuario?> Login(LoginUsuario dto)
        {
            using var sha256 = SHA256.Create();
            var senhaHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(dto.Senha)));

            var emailParam = new SqlParameter("@EMAIL", dto.Email);
            var senhaParam = new SqlParameter("@SENHA_HASH", senhaHash);

            var user = await _context.Usuarios
                .FromSqlRaw("SELECT * FROM TB_USUARIO WHERE EMAIL = @EMAIL AND SENHA_HASH = @SENHA_HASH",
                    emailParam, senhaParam)
                .FirstOrDefaultAsync();

            return user;
        }

    }
}
