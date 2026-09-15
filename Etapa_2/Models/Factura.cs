using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class Factura
{
    public int FacturaId { get; set; }

    public long NumeroFactura { get; set; }

    public string ClaveFiscal { get; set; } = null!;

    public long IdCliente { get; set; }

    public DateTime FechaEmision { get; set; }

    public decimal MontoTotal { get; set; }

    public decimal Impuesto { get; set; }

    public int Moneda { get; set; }

    public int Estado { get; set; }

    public byte[] Xml { get; set; } = null!;

    public string Referencia { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public virtual EstadoFactura EstadoNavigation { get; set; } = null!;

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Monedum MonedaNavigation { get; set; } = null!;

    public virtual ICollection<PagoFactura> PagoFacturas { get; set; } = new List<PagoFactura>();
}
