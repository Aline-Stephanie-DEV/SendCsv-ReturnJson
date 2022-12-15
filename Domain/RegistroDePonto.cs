using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class RegistroDePonto
{
    public int CodigoDoFuncionario { get; set; }
    public string? NomeDoFuncionario { get; set; }
    public decimal ValorHora { get; set; }
    public DateOnly Data { get; set; }
    public TimeSpan Entrada { get; set; }
    public TimeSpan Saida { get; set; }
    public (TimeSpan Inicio, TimeSpan Fim) Almoco { get; set; }

    public RegistroDePonto(int codigoDoFuncionario, string? nomeDoFuncionario, decimal valorHora, DateOnly data, TimeSpan entrada, TimeSpan saida, (TimeSpan Inicio, TimeSpan Fim) almoco)
    {
        CodigoDoFuncionario = codigoDoFuncionario;
        NomeDoFuncionario = nomeDoFuncionario;
        ValorHora = valorHora;
        Data = data;
        Entrada = entrada;
        Saida = saida;
        Almoco = almoco;
    }
}
