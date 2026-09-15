using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class EmpleadoRol
{
    public int IdColaborador { get; set; }

    public int IdRol { get; set; }

    public DateOnly FechaAsignacion { get; set; }

    public int UsuarioResponsable { get; set; }

    public bool Estado { get; set; }

    public virtual Empleado IdColaboradorNavigation { get; set; } = null!;

    public virtual Rol IdRolNavigation { get; set; } = null!;
}
