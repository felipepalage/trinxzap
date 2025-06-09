using TechSphere.Models.Agendamentos;

namespace TechSphere.Repository.Interface
{
    public interface IAgendamentoRepository
    {
        Task<IEnumerable<Agendamento>> GetAllAsync();
        Task<Agendamento?> GetByIdAsync(int id);
        Task<Agendamento> CreateAsync(Agendamento agendamento);
        Task UpdateAsync(Agendamento agendamento);
        Task DeleteAsync(int id);
    }
}
