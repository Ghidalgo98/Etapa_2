using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class ControlStock
{
    public int IdInventario { get; set; }

    public int IdProductoStock { get; set; }

    public int CantidadActual { get; set; }

    public DateOnly FechaActualizacion { get; set; }

    public virtual Producto IdProductoStockNavigation { get; set; } = null!;
}
