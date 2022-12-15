using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class RegistroDePonto
{
    public string CodigoDoFuncionario { get; set; }
    public string NomeDoFuncionario { get; set; }
    public string ValorHora { get; set; }
    public string Data { get; set; }
    public string Entrada { get; set; }
    public string Saida { get; set; }
    public string Almoco { get; set; }

    public RegistroDePonto(string codigoDoFuncionario, string nomeDoFuncionario, string valorHora, string data, string entrada, string saida, string almoco)
    {
        CodigoDoFuncionario = codigoDoFuncionario;
        NomeDoFuncionario = nomeDoFuncionario;
        ValorHora = valorHora;
        Data = data;
        Entrada = entrada;
        Saida = saida;
        Almoco = almoco;
    }

    public override string ToString()
    {
        return "Codigo:" + CodigoDoFuncionario +
            "Nome:" + NomeDoFuncionario +
            "Valor Hora:" + ValorHora +
            "Data:" + Data +
            "Entrada:" + Entrada +
            "Saida:" + Saida +
            "Almoco:" + Almoco;
    }
}
