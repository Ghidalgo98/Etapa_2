using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class Empleado
{
    public int IdColaborador { get; set; }

    public long IdEmpleado { get; set; }

    public int RolEmpleado { get; set; }

    public int IdTurno { get; set; }

    public int IdSalarios { get; set; }

    public int IdPuesto { get; set; }

    public virtual ICollection<ArqueoCaja> ArqueoCajas { get; set; } = new List<ArqueoCaja>();

    public virtual ICollection<EmpleadoRol> EmpleadoRols { get; set; } = new List<EmpleadoRol>();

    public virtual Turno IdTurnoNavigation { get; set; } = null!;

    public virtual ICollection<PagoFactura> PagoFacturas { get; set; } = new List<PagoFactura>();

    public virtual ICollection<Puesto> Puestos { get; set; } = new List<Puesto>();

    public virtual ICollection<Salario> Salarios { get; set; } = new List<Salario>();

    public virtual UsuarioEmpleado? UsuarioEmpleado { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
