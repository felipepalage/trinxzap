using TechSphere.Models.Agendamentos;
using TechSphere.Repository.Interface;
using TechSphere.Service.Interface;

namespace TechSphere.Service.Implementacoes
{
    public class ProfissionalService : IProfissionalService
    {
        private readonly IProfissionalRepository _repository;

        public ProfissionalService(IProfissionalRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Profissional>> GetProfissionaisAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Profissional?> GetProfissionalAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Profissional> AdicionarProfissionalAsync(Profissional profissional)
        {
            return await _repository.CreateAsync(profissional);
        }

        public async Task AtualizarProfissionalAsync(Profissional profissional)
        {
            await _repository.UpdateAsync(profissional);
        }

        public async Task RemoverProfissionalAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
