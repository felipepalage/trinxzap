namespace TechSphere.Models.Agendamentos
{
    public class HorarioProfissional
    {
        public int IdHorario { get; set; }
        public int IdProfissional { get; set; }
        public string DiaSemana { get; set; } = string.Empty;
        public TimeSpan HrInicio { get; set; }
        public TimeSpan HrFim { get; set; }
    }
}
