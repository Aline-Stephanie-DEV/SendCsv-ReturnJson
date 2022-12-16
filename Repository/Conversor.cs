using Domain;

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
                    int AnoVigente = int.Parse(ano);
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
            "Janeiro" => true,
            "Fevereiro" => true,
            "Março" => true,
            "Marco" => true,
            "Abril" => true,
            "Maio" => true,
            "Junho" => true,
            "Julho" => true,
            "Agosto" => true,
            "Setembro" => true,
            "Outubro" => true,
            "Novembro" => true,
            "Dezembro" => true,
            _ => false,
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
        Departamento departamento = new (nomeDepartamento, mes, ano);
        if (!departamentos.Contains(departamento))
        {
            departamentos.Add(departamento);
        }

        using StreamReader reader = new (caminho);
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
                RegistroDePonto registro = new (departamento, columnCodigo[i], columnFuncionario[i], columnValorHora[i], 
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
        CalculoDeGastos(registroDePontos);
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
        catch
        {
            Console.WriteLine("Dados Inválidos");
        }
    }

    public static void CalculoDeGastos(List<RegistroDePonto> registroDePontos)
    {
        foreach (RegistroDePonto registro in registroDePontos)
        {
            
        }
    }

}
