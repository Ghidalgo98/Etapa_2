using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class Monedum
{
    public int IdMoneda { get; set; }

    public string DescripcionMoneda { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<ArqueoCaja> ArqueoCajas { get; set; } = new List<ArqueoCaja>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<Salario> Salarios { get; set; } = new List<Salario>();
}
