namespace TechSphere.Models.Agendamentos
{
    public class Configuracao
    {
        public int IdConfig { get; set; }
        public int IdUsuario { get; set; }
        public string Chave { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;
    }
}
