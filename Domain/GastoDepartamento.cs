using System.Linq;
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

    public GastoDepartamento (string? departamento, string? mesVigencia, int anoVigencia, decimal totalPagar, decimal totalDescontos, decimal totalExtras, List<Funcionario>? funcionarios)
    {
        Departamento = departamento;
        MesVigencia = mesVigencia;
        AnoVigencia = anoVigencia;
        TotalPagar = totalPagar;
        TotalDescontos = totalDescontos;
        TotalExtras = totalExtras;
        Funcionarios = funcionarios;
    }
    public override string ToString()
    {
        return "Departamento:" + Departamento + "\n" + "Mes:" + MesVigencia + "\n" +
            "Ano:" + AnoVigencia + "\n" + "Total:" + TotalPagar + "\n" +
            "Descontos:" + TotalDescontos + "\n" + "Extras:" + TotalExtras + "\n" +
            "Funcionarios:" + string.Join(",", Funcionarios!);
    }
}
