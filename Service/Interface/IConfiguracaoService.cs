using TechSphere.Models;
using TechSphere.Models.Agendamentos;

namespace TechSphere.Service.Interface
{
    public interface IConfiguracaoService
    {
        Task<IEnumerable<Configuracao>> GetConfiguracoesAsync();
        Task<Configuracao?> GetConfiguracaoAsync(int id);
        Task<Configuracao> AdicionarConfiguracaoAsync(Configuracao config);
        Task AtualizarConfiguracaoAsync(Configuracao config);
        Task RemoverConfiguracaoAsync(int id);
    }
}
