namespace Domain;
public class Funcionario
{
    public string? Nome { get; set; }
    public int Codigo { get; set; }
    public decimal TotalReceber { get; set; }
    public TimeSpan HorasExtras { get; set; }
    public TimeSpan HorasDebito { get; set; }
    public int DiasFalta { get; set; }
    public int DiasExtras { get; set; }
    public int DiasTrabalhados { get; set; }

    public decimal Descontos { get; set; }
    public decimal Extras { get; set; }
    public Funcionario (string? nome, int codigo, decimal totalReceber, TimeSpan horasExtras, TimeSpan horasDebito, int diasFalta, int diasExtras, int diasTrabalhados)
    {
        Nome = nome;
        Codigo = codigo;
        TotalReceber = totalReceber;
        HorasExtras = horasExtras;
        HorasDebito = horasDebito;
        DiasFalta = diasFalta;
        DiasExtras = diasExtras;
        DiasTrabalhados = diasTrabalhados;
    }

    public override string ToString()
    {
        return "Nome:" + Nome + "\n" + "Codigo:" + Codigo + "\n" + "TotalReceber:" + TotalReceber + "\n" + 
            "Horas Extras:" + HorasExtras + "\n" + "Horas Debito:" + HorasDebito + "\n" + "Faltas:" + DiasFalta + "\n" +
            "Dias Extras:" + DiasExtras + "\n" + "Dias trabalhados:" + DiasTrabalhados + "\n";
    }
    public override bool Equals(object? obj)
    {
        return obj is Funcionario funcionario && funcionario.Codigo == Codigo;
    }
}
