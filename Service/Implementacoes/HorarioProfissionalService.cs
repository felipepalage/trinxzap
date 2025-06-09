using TechSphere.Models;
using TechSphere.Models.Agendamentos;
using TechSphere.Repository.Interface;
using TechSphere.Service.Interface;

namespace TechSphere.Service.Implementacoes
{
    public class HorarioProfissionalService : IHorarioProfissionalService
    {
        private readonly IHorarioProfissionalRepository _repository;

        public HorarioProfissionalService(IHorarioProfissionalRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<HorarioProfissional>> GetHorariosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<HorarioProfissional?> GetHorarioAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<HorarioProfissional> AdicionarHorarioAsync(HorarioProfissional horario)
        {
            return await _repository.CreateAsync(horario);
        }

        public async Task AtualizarHorarioAsync(HorarioProfissional horario)
        {
            await _repository.UpdateAsync(horario);
        }

        public async Task RemoverHorarioAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
