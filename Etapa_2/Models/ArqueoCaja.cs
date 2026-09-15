using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class ArqueoCaja
{
    public int IdArqueo { get; set; }

    public DateTime FechaArqueo { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraCierre { get; set; }

    public int EmpleadoId { get; set; }

    public int Moneda { get; set; }

    public decimal MontoInicial { get; set; }

    public decimal MontoRegistrado { get; set; }

    public decimal MontoFisico { get; set; }

    public decimal MontoElectrónico { get; set; }

    public decimal Diferencia { get; set; }

    public string Observaciones { get; set; } = null!;

    public bool Estado { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<ConciliacionDiarium> ConciliacionDiaria { get; set; } = new List<ConciliacionDiarium>();

    public virtual Empleado Empleado { get; set; } = null!;

    public virtual Monedum MonedaNavigation { get; set; } = null!;
}
