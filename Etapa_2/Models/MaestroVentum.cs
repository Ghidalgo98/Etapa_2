using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class MaestroVentum
{
    public int IdVenta { get; set; }

    public long IdCliente { get; set; }

    public DateOnly Fecha { get; set; }

    public decimal Total { get; set; }

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();
}
