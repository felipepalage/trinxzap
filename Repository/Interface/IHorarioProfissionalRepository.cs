using TechSphere.Models.Agendamentos;

namespace TechSphere.Repository.Interface
{
    public interface IHorarioProfissionalRepository
    {
        Task<IEnumerable<HorarioProfissional>> GetAllAsync();
        Task<HorarioProfissional?> GetByIdAsync(int id);
        Task<HorarioProfissional> CreateAsync(HorarioProfissional horario);
        Task UpdateAsync(HorarioProfissional horario);
        Task DeleteAsync(int id);
    }
}
