using TechSphere.Models.Agendamentos;

namespace TechSphere.Repository.Interface
{
    public interface IConfiguracaoRepository
    {
        Task<IEnumerable<Configuracao>> GetAllAsync();
        Task<Configuracao?> GetByIdAsync(int id);
        Task<Configuracao> CreateAsync(Configuracao config);
        Task UpdateAsync(Configuracao config);
        Task DeleteAsync(int id);
    }
}
