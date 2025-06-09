namespace TechSphere.Models.Agendamentos
{
    public class Profissional
    {
        public int IdProfissional { get; set; }
        public string NomeProfissional { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Especialidade { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
