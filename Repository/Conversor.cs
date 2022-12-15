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
        var columnCodigo = new List<string>();
        var columnFuncionario = new List<string>();
        var columnValorHora = new List<string>();
        var columnData = new List<string>();
        var columnEntrada = new List<string>();
        var columnSaida = new List<string>();
        var columnAlmoco = new List<string>();

        using var stream = new FileStream(caminho, FileMode.Open);
        using var reader = new StreamReader(stream);

        while (!reader.EndOfStream)
        {
            string[] splits = reader.ReadLine().Split(';');
            columnCodigo.Add(splits[0]);
            columnFuncionario.Add(splits[1]);
            columnValorHora.Add(splits[2]);
            columnData.Add(splits[3]);
            columnEntrada.Add(splits[4]);
            columnSaida.Add(splits[5]);
            columnAlmoco.Add(splits[6]);

            reader.Close();
        }
        Console.WriteLine("Column 1:");
        foreach (var element in columnFuncionario)
        {
            Console.WriteLine(element);
        }
        Console.WriteLine("Column 2:");
        foreach (var element in columnCodigo)
        {
            Console.WriteLine(element);
        }

    }
}
