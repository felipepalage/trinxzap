using TechSphere.Models.Agendamentos;
using TechSphere.Repository.Interface;
using TechSphere.Service.Interface;

namespace TechSphere.Service.Implementacoes
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;

        public AuthService(IAuthRepository repository)
        {
            _repository = repository;
        }

        public async Task<Usuario> RegistrarUsuarioAsync(RegistroUsuario dto)
        {
            return await _repository.Registrar(dto);
        }

        public async Task<Usuario?> LoginAsync(LoginUsuario dto)
        {
            return await _repository.Login(dto);
        }

        public async Task<bool> UsuarioExisteAsync(string email)
        {
            return await _repository.UsuarioExiste(email);
        }
    }
}
