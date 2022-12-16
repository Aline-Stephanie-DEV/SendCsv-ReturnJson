using Domain;

namespace Repository;
public class Conversor
{
    public static void GeraJson(string caminho)
    {
        ObtemDadosDoCsv(caminho);
        
    }
   
    public static bool AvaliaNomeDoArquivo(string filename)
    {

        bool tipoDeArquivo = filename.EndsWith(".csv");
        if (tipoDeArquivo == true)
        {
            var departamentos = new List<Departamento>();
            string[] nomeDoArquivo = filename.Split('-');
            string nomeDepartamento = nomeDoArquivo[0];
            string mes = nomeDoArquivo[1].Trim();
            string ano = nomeDoArquivo[2].Replace(".csv", "");

            if (nomeDepartamento.Length > 0 && EhMes(mes) == true)
            {
                try
                {
                    int.Parse(ano);

                    Departamento departamento = new(nomeDepartamento, mes, ano);
                    if (!departamentos.Contains(departamento))
                    {
                        departamentos.Add(departamento);
                    }
                    return true;

                }
                catch { return false; }
            }
        }
        return false;
    } 
    public static bool EhMes(string mes)
    {
        switch (mes)
        {
            case "Janeiro": return true;
            case "Fevereiro": return true;
            case "Março": return true;
            case "Marco": return true;
            case "Abril": return true;
            case "Maio": return true;
            case "Junho": return true;
            case "Julho": return true;
            case "Agosto": return true;
            case "Setembro": return true;
            case "Outubro": return true;
            case "Novembro": return true;
            case "Dezembro": return true;
            default: return false;
        }

    }
    public static void ObtemDadosDoCsv(string caminho)
    {
        int i;
        var columnCodigo = new List<string>();
        var columnFuncionario = new List<string>();
        var columnValorHora = new List<string>();
        var columnData = new List<string>();
        var columnEntrada = new List<string>();
        var columnSaida = new List<string>();
        var columnAlmoco = new List<string>();
        var registrosDePonto = new List<RegistroDePonto>();

        using StreamReader reader = new StreamReader(caminho);
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
                RegistroDePonto registro = new (columnCodigo[i], columnFuncionario[i], columnValorHora[i], 
                    columnData[i], columnEntrada[i], columnSaida[i], columnAlmoco[i]);
                ValidaDados(registro);
                registrosDePonto.Add(registro);
                Console.WriteLine(registro.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
    public static void ValidaDados(RegistroDePonto registro)
    {
        try
        {
            string[] almoco = registro.Almoco.Split('-');
            int codigo = int.Parse(registro.CodigoDoFuncionario);
            decimal valorHora = decimal.Parse(registro.ValorHora);
            DateOnly data = DateOnly.Parse(registro.Data);
            TimeSpan entrada = TimeSpan.Parse(registro.Entrada);
            TimeSpan saida = TimeSpan.Parse(registro.Saida);
            TimeSpan inicio = TimeSpan.Parse(almoco[0]);
            TimeSpan fim = TimeSpan.Parse(almoco[1]);
        }
        catch{
            Console.WriteLine("Dados Inválidos");
        }
    }

}
