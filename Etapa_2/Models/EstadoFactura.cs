using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class EstadoFactura
{
    public int IdEstadoFactura { get; set; }

    public string DescripcionEstadoFactura { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<PagoFactura> PagoFacturas { get; set; } = new List<PagoFactura>();
}
