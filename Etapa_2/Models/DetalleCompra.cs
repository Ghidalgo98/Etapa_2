using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class DetalleCompra
{
    public int IdDetalle { get; set; }

    public int IdCompra { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }

    public virtual MaestroCompra IdCompraNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
