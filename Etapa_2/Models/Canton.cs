using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class Canton
{
    public int IdCantón { get; set; }

    public int ProvinciaIdProvincia { get; set; }

    public int ProvinciaPaisIdPais { get; set; }

    public string? DescripciónCanton { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Distrito> Distritos { get; set; } = new List<Distrito>();

    public virtual Provincium Provincium { get; set; } = null!;
}
