using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class Turno
{
    public int IdTurno { get; set; }

    public string DescripcionTurno { get; set; } = null!;

    public string Horario { get; set; } = null!;

    public bool HoraRoja { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
