using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class Conversor
{

    public static void GeraJson(string caminho)
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

        using (StreamReader reader = new StreamReader(caminho))
        {
            reader.Read();
            do
            {
                string[] splits = reader.ReadLine().Split(';');
                columnCodigo.Add(splits[0]);
                columnFuncionario.Add(splits[1]);
                columnValorHora.Add(splits[2]);
                columnData.Add(splits[3]);
                columnEntrada.Add(splits[4]);
                columnSaida.Add(splits[5]);
                columnAlmoco.Add(splits[6]);

            } while (!reader.EndOfStream);

            reader.Close();

            for (i=1; i < columnCodigo.Count; i++)
            {
                RegistroDePonto registro = new RegistroDePonto(columnCodigo[i], columnFuncionario[i], columnValorHora[i], columnData[i], columnEntrada[i], columnSaida[i], columnAlmoco[i]);
                registrosDePonto.Add(registro);

                Console.WriteLine(registro.ToString());

            }
        }
    }
}
