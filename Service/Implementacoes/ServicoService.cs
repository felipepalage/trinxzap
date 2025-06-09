using TechSphere.Models;
using TechSphere.Models.Agendamentos;
using TechSphere.Repository.Interface;
using TechSphere.Service.Interface;

namespace TechSphere.Service.Implementacoes
{
    public class ServicoService : IServicoService
    {
        private readonly IServicoRepository _repository;

        public ServicoService(IServicoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Servico>> GetServicosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Servico?> GetServicoAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Servico> AdicionarServicoAsync(Servico servico)
        {
            return await _repository.CreateAsync(servico);
        }

        public async Task AtualizarServicoAsync(Servico servico)
        {
            await _repository.UpdateAsync(servico);
        }

        public async Task RemoverServicoAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
