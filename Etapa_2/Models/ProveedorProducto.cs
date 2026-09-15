using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class ProveedorProducto
{
    public long IdProveedor { get; set; }

    public int IdProducto { get; set; }

    public decimal PrecioCompra { get; set; }

    public DateTime FechaUltimaCompra { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;
}
