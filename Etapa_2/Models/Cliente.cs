using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class Cliente
{
    public long IdCliente { get; set; }

    public int TipoCliente { get; set; }

    public string DescripcionCliente { get; set; } = null!;

    public int CategoriaCliente { get; set; }

    public long IdPersona { get; set; }

    public int Estado { get; set; }

    public string Observacion { get; set; } = null!;

    public virtual EstadoCliente EstadoNavigation { get; set; } = null!;

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual PersonaFisica IdPersonaNavigation { get; set; } = null!;

    public virtual TipoCliente TipoCliente1 { get; set; } = null!;

    public virtual SubcategoriaCliente TipoClienteNavigation { get; set; } = null!;
}
