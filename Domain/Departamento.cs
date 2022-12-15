﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class Departamento
{
    public string? NomeDoDepartamento { get; set; }
    public DateTime MesVigente { get; set; }
    public DateTime AnoVigente { get; set; }

    public Departamento(string? nomeDoDepartamento, DateTime mesVigente, DateTime anoVigente)
    {
        NomeDoDepartamento = nomeDoDepartamento;
        MesVigente = mesVigente;
        AnoVigente = anoVigente;
    }
}