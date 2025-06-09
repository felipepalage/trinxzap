using TechSphere.Models;
using TechSphere.Models.Agendamentos;
using TechSphere.Repository.Interface;
using TechSphere.Service.Interface;

namespace TechSphere.Service.Implementacoes
{
    public class ConfiguracaoService : IConfiguracaoService
    {
        private readonly IConfiguracaoRepository _repository;

        public ConfiguracaoService(IConfiguracaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Configuracao>> GetConfiguracoesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Configuracao?> GetConfiguracaoAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Configuracao> AdicionarConfiguracaoAsync(Configuracao config)
        {
            return await _repository.CreateAsync(config);
        }

        public async Task AtualizarConfiguracaoAsync(Configuracao config)
        {
            await _repository.UpdateAsync(config);
        }

        public async Task RemoverConfiguracaoAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
