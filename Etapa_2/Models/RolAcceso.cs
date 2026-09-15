using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class RolAcceso
{
    public int IdRolAcceso { get; set; }

    public string? Descripcion { get; set; }

    public ulong? Estado { get; set; }

    public virtual ICollection<AccesoUsuario> AccesoUsuarios { get; set; } = new List<AccesoUsuario>();
}
