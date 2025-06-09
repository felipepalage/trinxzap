using TechSphere.Models;
using TechSphere.Models.Agendamentos;
using TechSphere.Repository.Interface;
using TechSphere.Service.Interface;

namespace TechSphere.Service.Implementacoes
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _repository;

        public AgendamentoService(IAgendamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Agendamento>> GetAgendamentosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Agendamento?> GetAgendamentoAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Agendamento> AdicionarAgendamentoAsync(Agendamento agendamento)
        {
            return await _repository.CreateAsync(agendamento);
        }

        public async Task AtualizarAgendamentoAsync(Agendamento agendamento)
        {
            await _repository.UpdateAsync(agendamento);
        }

        public async Task RemoverAgendamentoAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
