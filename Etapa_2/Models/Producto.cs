using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string DescripciónProducto { get; set; } = null!;

    public decimal PrecioCompra { get; set; }

    public decimal PrecioVenta { get; set; }

    public int IdCategoria { get; set; }

    public int UnidadMedida { get; set; }

    public int Condicion { get; set; }

    public int Estado { get; set; }

    public virtual CondicionProducto CondicionNavigation { get; set; } = null!;

    public virtual ICollection<ControlStock> ControlStocks { get; set; } = new List<ControlStock>();

    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    public virtual CategoriaProducto IdCategoriaNavigation { get; set; } = null!;

    public virtual ICollection<ProveedorProducto> ProveedorProductos { get; set; } = new List<ProveedorProducto>();

    public virtual UnidadMedidum UnidadMedidaNavigation { get; set; } = null!;
}
