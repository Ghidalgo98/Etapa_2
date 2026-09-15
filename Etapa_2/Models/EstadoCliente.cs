using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class EstadoCliente
{
    public int IdEstadoCliente { get; set; }

    public string DescripciónEstadoCliente { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
