using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class MaestroCompra
{
    public int IdCompra { get; set; }

    public int IdProveedor { get; set; }

    public DateOnly Fecha { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Total { get; set; }

    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();
}
