using System.Text.Json.Serialization;

namespace Domain;
public class GastoDepartamento
{
    public string? Departamento { get; set; }
    public string? MesVigencia { get; set; }
    public int AnoVigencia { get; set; }
    public decimal TotalPagar { get; set; }
    public decimal TotalDescontos { get; set; }
    public decimal TotalExtras { get; set; }
    public List<Funcionario>? Funcionarios { get; set; }
}
