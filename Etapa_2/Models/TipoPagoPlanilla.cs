using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class TipoPagoPlanilla
{
    public int IdTipoPagoPlanilla { get; set; }

    public string DescripcionTipoPagoPlanilla { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Salario> Salarios { get; set; } = new List<Salario>();
}
