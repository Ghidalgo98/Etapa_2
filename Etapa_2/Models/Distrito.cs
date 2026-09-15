using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class Distrito
{
    public int IdDistrito { get; set; }

    public string DescripcionDistrito { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Direccion> Direccions { get; set; } = new List<Direccion>();
}
