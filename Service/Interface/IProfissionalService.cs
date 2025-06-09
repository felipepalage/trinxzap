using TechSphere.Models.Agendamentos;

namespace TechSphere.Service.Interface
{
    public interface IProfissionalService
    {
        Task<IEnumerable<Profissional>> GetProfissionaisAsync();
        Task<Profissional?> GetProfissionalAsync(int id);
        Task<Profissional> AdicionarProfissionalAsync(Profissional profissional);
        Task AtualizarProfissionalAsync(Profissional profissional);
        Task RemoverProfissionalAsync(int id);
    }
}
