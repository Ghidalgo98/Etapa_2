using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class AccesoUsuario
{
    public int RolAccesoIdRolAcceso { get; set; }

    public long UsuarioPersonaFisicaId { get; set; }

    public ulong? Estado { get; set; }

    public virtual RolAcceso RolAccesoIdRolAccesoNavigation { get; set; } = null!;

    public virtual Usuario UsuarioPersonaFisica { get; set; } = null!;
}
