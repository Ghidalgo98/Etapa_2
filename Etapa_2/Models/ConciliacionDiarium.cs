using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class ConciliacionDiarium
{
    public int ConciliacionId { get; set; }

    public int ArqueoId { get; set; }

    public decimal Ingresos { get; set; }

    public decimal Egresos { get; set; }

    public decimal SaldoFinal { get; set; }

    public decimal Diferencia { get; set; }

    public DateTime FechaConciliacion { get; set; }

    public virtual ArqueoCaja Arqueo { get; set; } = null!;
}
