using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class Puesto
{
    public int IdPuesto { get; set; }

    public int IdEmpleado { get; set; }

    public string NombrePuesto { get; set; } = null!;

    public int IdDepartamento { get; set; }

    public string DescripcionPuesto { get; set; } = null!;

    public byte[] NivelJerarquicos { get; set; } = null!;

    public bool Estado { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual ICollection<Salario> Salarios { get; set; } = new List<Salario>();
}
