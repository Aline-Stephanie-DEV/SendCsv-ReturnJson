namespace Domain;
public class RegistroDePonto
{
    public Departamento Departamento { get; set; }
    public string CodigoDoFuncionario { get; set; }
    public string NomeDoFuncionario { get; set; }
    public string ValorHora { get; set; }
    public string Data { get; set; }
    public string Entrada { get; set; }
    public string Saida { get; set; }
    public string Almoco { get; set; }

    public RegistroDePonto(Departamento departamento, string codigoDoFuncionario, string nomeDoFuncionario, string valorHora, string data, 
        string entrada, string saida, string almoco)
    {
        Departamento = departamento;
        CodigoDoFuncionario = codigoDoFuncionario;
        NomeDoFuncionario = nomeDoFuncionario;
        ValorHora = valorHora;
        Data = data;
        Entrada = entrada;
        Saida = saida;
        Almoco = almoco;
    }
}
