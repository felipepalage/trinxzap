using TechSphere.Models.Agendamentos;

namespace TechSphere.Service.Interface
{
    public interface IAgendamentoService
    {
        Task<IEnumerable<Agendamento>> GetAgendamentosAsync();
        Task<Agendamento?> GetAgendamentoAsync(int id);
        Task<Agendamento> AdicionarAgendamentoAsync(Agendamento agendamento);
        Task AtualizarAgendamentoAsync(Agendamento agendamento);
        Task RemoverAgendamentoAsync(int id);
    }
}
