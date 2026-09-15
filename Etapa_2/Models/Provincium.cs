using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class Provincium
{
    public int IdProvincia { get; set; }

    public string DescripcionProvincia { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Direccion> Direccions { get; set; } = new List<Direccion>();
}
