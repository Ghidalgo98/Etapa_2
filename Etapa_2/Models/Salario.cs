using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class Salario
{
    public int IdSalario { get; set; }

    public int IdEmpleado { get; set; }

    public int IdPuesto { get; set; }

    public string DescripcionSalario { get; set; } = null!;

    public int TipoPago { get; set; }

    public int Moneda { get; set; }

    public decimal SalarioBruto { get; set; }

    public bool Estado { get; set; }

    public int UsuarioRegistro { get; set; }

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual Puesto IdPuestoNavigation { get; set; } = null!;

    public virtual Monedum MonedaNavigation { get; set; } = null!;

    public virtual TipoPagoPlanilla TipoPagoNavigation { get; set; } = null!;
}
