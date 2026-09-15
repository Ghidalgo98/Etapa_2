using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class UnidadMedidum
{
    public int IdUnidad { get; set; }

    public string? DescripcionUnidad { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
