using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class Utils
{
    public static bool EhMes(string mes)
    {
        string[] meses = {"Janeiro", "Fevereiro","Março", "Marco", "Abril", "Maio", "Junho", "Julho",
            "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"};
        return meses.Contains(mes);
    }
    public static int Meses(string mes)
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
    public static string Meses(int mes)
    {
        return mes switch
        {
            1 => "Janeiro",
            2 => "Fevereiro",
            3 => "Março",
            4 => "Abril",
            5 => "Maio",
            6 => "Junho",
            7 => "Julho",
            8 => "Agosto",
            9 => "Setembro",
            10 => "Outubro",
            11 => "Novembro",
            12 => "Dezembro",
            _ => "",
        };
    }
}
