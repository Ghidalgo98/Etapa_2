using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class UsuarioEmpleado
{
    public int IdEmpleado { get; set; }

    public DateTime FechaRegistro { get; set; }

    public DateTime FechaModificacion { get; set; }

    public bool Estado { get; set; }

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;
}
