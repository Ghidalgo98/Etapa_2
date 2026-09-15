using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class DetalleVentum
{
    public int IdDetalle { get; set; }

    public int IdVenta { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual MaestroVentum IdVentaNavigation { get; set; } = null!;
}
