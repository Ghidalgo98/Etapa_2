using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class Distrito
{
    public int IdDistrito { get; set; }

    public int CantonIdCantón { get; set; }

    public int CantonProvinciaIdProvincia { get; set; }

    public int CantonProvinciaPaisIdPais { get; set; }

    public string DescripcionDistrito { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual Canton Canton { get; set; } = null!;
}
