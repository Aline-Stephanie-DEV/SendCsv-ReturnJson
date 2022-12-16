namespace Domain;

public class Departamento
{
    public string? NomeDoDepartamento { get; set; }
    public string MesVigente { get; set; }
    public string AnoVigente { get; set; }
    public Departamento(string? nomeDoDepartamento, string mesVigente, string anoVigente)
    {
        NomeDoDepartamento = nomeDoDepartamento;
        MesVigente = mesVigente;
        AnoVigente = anoVigente;
    }
}
