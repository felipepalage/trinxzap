using TechSphere.Models.Agendamentos;

namespace TechSphere.Repository.Interface
{
    public interface IProfissionalRepository
    {
        Task<IEnumerable<Profissional>> GetAllAsync();
        Task<Profissional?> GetByIdAsync(int id);
        Task<Profissional> CreateAsync(Profissional profissional);
        Task UpdateAsync(Profissional profissional);
        Task DeleteAsync(int id);
    }
}
