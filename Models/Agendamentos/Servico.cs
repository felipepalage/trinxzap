namespace TechSphere.Models.Agendamentos;

public class Servico
{
    public int IdServico { get; set; }
    public string NomeServico { get; set; } = string.Empty;
    public decimal ValorPreco { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public bool Ativo { get; set; }
}
