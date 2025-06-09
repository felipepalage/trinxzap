using TechSphere.Models;
using TechSphere.Models.Agendamentos;

namespace TechSphere.Service.Interface
{
    public interface IServicoService
    {
        Task<IEnumerable<Servico>> GetServicosAsync();
        Task<Servico?> GetServicoAsync(int id);
        Task<Servico> AdicionarServicoAsync(Servico servico);
        Task AtualizarServicoAsync(Servico servico);
        Task RemoverServicoAsync(int id);
    }
}
