using System.ComponentModel.DataAnnotations;

namespace TechSphere.Models.Agendamentos
{
    public class Agendamento
    {
        [Key]
        public int IdAgendamento { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public string NomeServico { get; set; } = string.Empty;
        public DateTime DataAgendamento { get; set; }
        public string Status { get; set; } = string.Empty;
        public int IdProfissional { get; set; }

    };
}
