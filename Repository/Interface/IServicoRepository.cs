using TechSphere.Models.Agendamentos;

namespace TechSphere.Repository.Interface
{
    public interface IServicoRepository
    {
        Task<IEnumerable<Servico>> GetAllAsync();
        Task<Servico?> GetByIdAsync(int id);
        Task<Servico> CreateAsync(Servico servico);
        Task UpdateAsync(Servico servico);
        Task DeleteAsync(int id);
    }
}
