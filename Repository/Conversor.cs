using Domain;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Globalization;
using System.Security.Cryptography;

namespace Repository;
public class Conversor
{
    public static void GeraJson(string caminho, string filename)
    {
        ObtemDadosDoCsv(caminho, filename);
    }
    public static bool AvaliaNomeDoArquivo(string filename)
    {
        bool tipoDeArquivo = filename.EndsWith(".csv");
        if (tipoDeArquivo == true)
        {
            string[] nomeDoArquivo = filename.Split('-');
            string nomeDepartamento = nomeDoArquivo[0].Trim();
            string mes = nomeDoArquivo[1].Trim();
            string ano = nomeDoArquivo[2].Trim().Replace(".csv", "");

            if (nomeDepartamento.Length > 0 && EhMes(mes) == true)
            {
                try
                {
                    int.Parse(ano);
                    return true;
                }
                catch { return false; }
            }
        }
        return false;
    }
    public static bool EhMes(string mes)
    {
        return mes switch
        {
            "Janeiro" or "Fevereiro" or "Março" or "Marco" or "Abril" or "Maio" or "Junho" or "Julho" or 
            "Agosto" or "Setembro" or "Outubro" or "Novembro" or "Dezembro" => true,
            _ => false,
        };
    }
    public static int Meses (string mes)
    {
        return mes switch
        {
            "Janeiro" => 1,
            "Fevereiro" => 2,
            "Março" => 3,
            "Marco" => 3,
            "Abril" => 4,
            "Maio" => 5,
            "Junho" => 6,
            "Julho" => 7,
            "Agosto" => 8,
            "Setembro" => 9,
            "Outubro" => 10,
            "Novembro" => 11,
            "Dezembro" => 12,
            _ => 0,
        };
    }
    public static void ObtemDadosDoCsv(string caminho, string filename)
    {
        int i;
        var departamentos = new List<Departamento>();
        var registroDePontos = new List<RegistroDePonto>();
        var columnCodigo = new List<string>();
        var columnFuncionario = new List<string>();
        var columnValorHora = new List<string>();
        var columnData = new List<string>();
        var columnEntrada = new List<string>();
        var columnSaida = new List<string>();
        var columnAlmoco = new List<string>();

        string[] nomeDoArquivo = filename.Split('-');

        string nomeDepartamento = nomeDoArquivo[0].Trim();
        string mes = nomeDoArquivo[1].Trim();
        string ano = nomeDoArquivo[2].Trim().Replace(".csv", "");
        
        int dia = 1;
        int anoVigente = int.Parse(ano);
        int mesVigente = Meses(mes);
        DateTime dataVigente = new (anoVigente , mesVigente, dia);

        Departamento departamento = new(nomeDepartamento, dataVigente);
        if (!departamentos.Contains(departamento))
        {
            departamentos.Add(departamento);
        }

        using StreamReader reader = new(caminho);
        reader.Read();
        do
        {
            string? linha = reader.ReadLine();
            if (linha == null) { break; }
            string[] splits = linha.Split(';');
            columnCodigo.Add(splits[0]);
            columnFuncionario.Add(splits[1]);
            columnValorHora.Add(splits[2]);
            columnData.Add(splits[3]);
            columnEntrada.Add(splits[4]);
            columnSaida.Add(splits[5]);
            columnAlmoco.Add(splits[6]);

        } while (!reader.EndOfStream);

        reader.Close();

        for (i = 1; i < columnCodigo.Count; i++)
        {
            try
            {
                RegistroDePonto registro = new(departamento, columnCodigo[i], columnFuncionario[i], columnValorHora[i],
                    columnData[i], columnEntrada[i], columnSaida[i], columnAlmoco[i]);
                ValidaDados(registro);
                registroDePontos.Add(registro);
                Console.WriteLine(registro.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        CalculoDeGastos(registroDePontos, departamento);
    }
    public static void ValidaDados(RegistroDePonto registro)
    {
        try
        {
            string[] almoco = registro.Almoco.Split('-');
            int.Parse(registro.CodigoDoFuncionario);
            decimal.Parse(registro.ValorHora);
            DateOnly.Parse(registro.Data);
            TimeSpan.Parse(registro.Entrada);
            TimeSpan.Parse(registro.Saida);
            TimeSpan.Parse(almoco[0]);
            TimeSpan.Parse(almoco[1]);
        }
        catch
        {
            Console.WriteLine("Dados Inválidos");
        }
    }

    public static void CalculoDeGastos(List<RegistroDePonto> registroDePontos, Departamento departamento)
    {
        int mes = departamento.DataVigente.Month;
        int ano = departamento.DataVigente.Year;
        string primeiroDiaMes = departamento.DataVigente.DayOfWeek.ToString();
        int diasNoMes = System.DateTime.DaysInMonth(ano, mes);

        int diasDeTrabalho;
        if (diasNoMes == 28)
        {
            diasDeTrabalho = 20;
        }
        else if (diasNoMes == 29)
        {
            diasDeTrabalho = primeiroDiaMes switch
            {
                "Sunday" or "Saturday" => 20,
                _ => 21,
            };
        }
        else if (diasNoMes == 30)
        {
            diasDeTrabalho = primeiroDiaMes switch
            {
                "Saturday" => 20,
                "Sunday" or "Friday" => 21,
                _ => 22,
            };
        }
        else
        {
            diasDeTrabalho = primeiroDiaMes switch
            {
                "Friday" or "Saturday" => 21,
                "Sunday" or "Thursday" => 22,
                _ => 23,
            };
        }
        
        foreach (RegistroDePonto registro in registroDePontos)
        {
            string codigoFunc = registro.CodigoDoFuncionario;
            string nomeFuncionario = registro.NomeDoFuncionario;
            List<RegistroDePonto> listaDePontosFunc = new();
            
            for (int i = 0; i < registroDePontos.Count; i++)
            {
                if(registro.CodigoDoFuncionario == codigoFunc)
                {
                    listaDePontosFunc.Add(registro);
                }
            }
            int diasTrabalhados = listaDePontosFunc.Count;
            int diferencaDias = diasDeTrabalho - diasTrabalhados;
            int diasExtras; int diasFalta;
            if (diferencaDias > 0) { diasFalta = diferencaDias; diasExtras = 0; }
            if (diferencaDias < 0) { diasFalta = 0; diasExtras = diferencaDias; }
            else { diasFalta = 0; diasExtras = 0; }

            int codigoFuncionario = int.Parse(codigoFunc);
            var horasTotais = new List<TimeSpan>();
            foreach (RegistroDePonto ponto in listaDePontosFunc)
            {
                string[] almoco = ponto.Almoco.Split('-');
                TimeSpan horasDia = TimeSpan.Parse(ponto.Saida) - TimeSpan.Parse(almoco[1]) + (TimeSpan.Parse(almoco[0]) - TimeSpan.Parse(ponto.Entrada));
                horasTotais.Add(horasDia);
            }
            TimeSpan horasFeitas = horasTotais.Aggregate((horasDia, next) => horasDia + next);

            //TimeSpan horasExtras;
            //TimeSpan horasDebito;

            //decimal valorHora = decimal.Parse(registro.ValorHora);

            //decimal descontos = horasDebito * valorHora;
            //decimal extras = horasExtras * valorHora;
            //decimal totalReceber = diasDeTrabalho * 9 * valorHora - descontos + extras;

            //Funcionario funcionario = new(nomeFuncionario, codigoFuncionario, totalReceber,
            //horasExtras, horasDebito, diasFalta, diasExtras, diasTrabalhados);

        }

    }
}

