namespace Domain;
public class Departamento
{
    public string? NomeDoDepartamento { get; set; }
    public DateTime DataVigente { get; set; }
    public Departamento(string? nomeDoDepartamento, DateTime dataVigente)
    {
        NomeDoDepartamento = nomeDoDepartamento;
        DataVigente = dataVigente;
    }
}
