using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class PagoFactura
{
    public int IdPago { get; set; }

    public int FacturaId { get; set; }

    public int MedioPago { get; set; }

    public decimal Monto { get; set; }

    public DateTime FechaPago { get; set; }

    public int UsuarioId { get; set; }

    public string Referencia { get; set; } = null!;

    public int Estado { get; set; }

    public virtual EstadoFactura EstadoNavigation { get; set; } = null!;

    public virtual Factura Factura { get; set; } = null!;

    public virtual TipoPagoFactura MedioPagoNavigation { get; set; } = null!;

    public virtual Empleado Usuario { get; set; } = null!;
}
