using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class TipoPagoFactura
{
    public int IdTipoPagoFactura { get; set; }

    public string DescripcionTipoPagoFactura { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<PagoFactura> PagoFacturas { get; set; } = new List<PagoFactura>();
}
