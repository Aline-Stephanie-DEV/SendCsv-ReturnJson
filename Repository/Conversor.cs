using Domain;
using System.Text.Json;
using System.Text.Json.Serialization;

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

            if (nomeDepartamento.Length > 0 && Utils.EhMes(mes) == true)
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
    public static void ObtemDadosDoCsv(string caminho, string filename)
    {
        var departamentos = new List<Departamento>();
        var registroDePontos = new List<RegistroDePonto>();
        string[] nomeDoArquivo = filename.Split('-');
        string nomeDepartamento = nomeDoArquivo[0].Trim();
        string mes = nomeDoArquivo[1].Trim();
        string ano = nomeDoArquivo[2].Trim().Replace(".csv", "");
        int dia = 1;
        int anoVigente = int.Parse(ano);
        int mesVigente = Utils.Meses(mes);
        DateTime dataVigente = new (anoVigente , mesVigente, dia);

        Departamento departamento = new(nomeDepartamento, dataVigente);
        if (!departamentos.Contains(departamento))
        {
            departamentos.Add(departamento);
        }

        using StreamReader reader = new(caminho);
        reader.Read();
        string? linha = reader.ReadLine();
        if (linha is null){ throw new Exception("Arquivo vazio"); }
        List<string> splits = linha.Split(';').ToList();
        int posicaoColunaCodigo = splits.IndexOf("Codigo");
        int posicaoColunaFuncionario = splits.IndexOf("Nome");
        int posicaoColunaValorHora = splits.IndexOf("Valor Hora");
        int posicaoColunaData = splits.IndexOf("Data");
        int posicaoColunaEntrada = splits.IndexOf("Entrada");
        int posicaoColunaSaida = splits.IndexOf("Saida");
        int posicaoColunaAlmoco = splits.IndexOf("Almoco");

        while (!reader.EndOfStream)
        {
            linha = reader.ReadLine();
            if (linha is null) { throw new Exception("Arquivo vazio"); }
            splits = linha.Split(';').ToList();
            RegistroDePonto registro = new(departamento, splits[posicaoColunaCodigo], splits[posicaoColunaFuncionario],
                splits[posicaoColunaValorHora], splits[posicaoColunaData], splits[posicaoColunaEntrada],
                splits[posicaoColunaSaida], splits[posicaoColunaAlmoco]);
            ValidaDados(registro);
            registroDePontos.Add(registro);
        }
        reader.Close();

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
            throw new Exception("Dados Inválidos");
        }
    }
    public static void CalculoDeGastos(List<RegistroDePonto> registroDePontos, Departamento departamento)
    {
        int mes = departamento.DataVigente.Month;
        int ano = departamento.DataVigente.Year;
        var primeiroDiaMes = departamento.DataVigente.DayOfWeek;
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
                DayOfWeek.Sunday or DayOfWeek.Saturday => 20,
                _ => 21,
            };
        }
        else if (diasNoMes == 30)
        {
            diasDeTrabalho = primeiroDiaMes switch
            {
                DayOfWeek.Saturday => 20,
                DayOfWeek.Sunday or DayOfWeek.Friday => 21,
                _ => 22,
            };
        }
        else
        {
            diasDeTrabalho = primeiroDiaMes switch
            {
                DayOfWeek.Friday or DayOfWeek.Saturday => 21,
                DayOfWeek.Sunday or DayOfWeek.Thursday => 22,
                _ => 23,
            };
        }
        var funcionarios = new List<Funcionario>();

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
            int codigoFuncionario = int.Parse(codigoFunc);

            //Calculo dos Dias
            int diasTrabalhados = listaDePontosFunc.Count;
            int diferencaDias = diasDeTrabalho - diasTrabalhados;
            int diasExtras; 
            int diasFalta;
            if (diferencaDias > 0) { diasFalta = diferencaDias; diasExtras = 0; }
            if (diferencaDias < 0) { diasFalta = 0; diasExtras = diferencaDias; }
            else { diasFalta = 0; diasExtras = 0; }

            //Calculo de horas
            var horasTotais = new List<TimeSpan>();
            foreach (RegistroDePonto ponto in listaDePontosFunc)
            {
                string[] almoco = ponto.Almoco.Split('-');
                TimeSpan horasDia = TimeSpan.Parse(ponto.Saida) - TimeSpan.Parse(almoco[1]) + (TimeSpan.Parse(almoco[0]) - TimeSpan.Parse(ponto.Entrada));
                horasTotais.Add(horasDia);
            }
            TimeSpan horasFeitas = horasTotais.Aggregate((horasDia, next) => horasDia + next);
            TimeSpan horasEsperadas = new TimeSpan(09, 00, 00) * diasDeTrabalho;
;           TimeSpan diferancaHoras = horasEsperadas - horasFeitas;
            TimeSpan horasExtras;
            TimeSpan horasDebito;
            if (diferancaHoras > TimeSpan.Zero) { horasExtras = TimeSpan.Zero; horasDebito = diferancaHoras; }
            if (diferancaHoras < TimeSpan.Zero) { horasExtras = diferancaHoras; horasDebito = TimeSpan.Zero; }
            else { horasExtras = TimeSpan.Zero; horasDebito = TimeSpan.Zero; }

            //Calculo do valor a receber
            decimal valorHora = decimal.Parse(registro.ValorHora);
            decimal horasDescontadas = Convert.ToDecimal(horasDebito.TotalHours);
            decimal horasAcrescidas = Convert.ToDecimal(horasExtras.TotalHours);
            decimal descontos = horasDescontadas * valorHora;
            decimal extras = horasAcrescidas * valorHora;
            decimal totalReceber = diasDeTrabalho * 9 * valorHora - descontos + extras;

            //Salva informacoes
            Funcionario funcionario = new(nomeFuncionario, codigoFuncionario, totalReceber, 
                horasExtras, horasDebito, diasFalta, diasExtras, diasTrabalhados)
            {
                Extras = extras,
                Descontos= descontos
            };

            if (!funcionarios.Contains(funcionario))
            {
                funcionarios.Add(funcionario);
            }
        }

        decimal totalPagar = funcionarios.Sum(f => f.TotalReceber); 
        decimal totalDescontos = funcionarios.Sum(d => d.Descontos);
        decimal totalExtras = funcionarios.Sum(f => f.Extras);

        string? nomeDepartamento = departamento.NomeDoDepartamento;
        GastoDepartamento gastoDepartamento = new(nomeDepartamento, Utils.Meses(mes), ano, totalPagar, 
            totalDescontos , totalExtras, funcionarios);
        MontaJson(gastoDepartamento);
    }
    public static void MontaJson(GastoDepartamento gastoDepartamento)
    {
        JsonSerializerOptions _options = new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

        var options = new JsonSerializerOptions(_options)
        {
            WriteIndented = true
        };
        var jsonString = JsonSerializer.Serialize(gastoDepartamento);
        File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files\\GastosDoDepartamento.json"), jsonString);

    }
}

