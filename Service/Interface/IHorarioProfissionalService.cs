using TechSphere.Models.Agendamentos;

namespace TechSphere.Service.Interface
{
    public interface IHorarioProfissionalService
    {
        Task<IEnumerable<HorarioProfissional>> GetHorariosAsync();
        Task<HorarioProfissional?> GetHorarioAsync(int id);
        Task<HorarioProfissional> AdicionarHorarioAsync(HorarioProfissional horario);
        Task AtualizarHorarioAsync(HorarioProfissional horario);
        Task RemoverHorarioAsync(int id);
    }
}
