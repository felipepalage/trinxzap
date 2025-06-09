using TechSphere.Models.Agendamentos;

namespace TechSphere.Service.Interface
{
    public interface IAuthService
    {
        Task<Usuario> RegistrarUsuarioAsync(RegistroUsuario dto);
        Task<Usuario?> LoginAsync(LoginUsuario dto);
        Task<bool> UsuarioExisteAsync(string email);
    }
}
