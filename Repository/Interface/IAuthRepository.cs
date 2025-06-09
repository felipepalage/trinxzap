using TechSphere.Models.Agendamentos;

namespace TechSphere.Repository.Interface
{
    public interface IAuthRepository
    {
       Task<bool> UsuarioExiste(string email);
       Task<Usuario> Registrar(RegistroUsuario dto);
        Task<Usuario?> Login(LoginUsuario dto);


    }
}
