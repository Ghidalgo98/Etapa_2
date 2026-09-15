using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class TipoCliente
{
    public int IdTipoCliente { get; set; }

    public string DescripcionTipoCliente { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
