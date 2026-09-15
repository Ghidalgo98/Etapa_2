using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class Telefono
{
    public int IdTelefono { get; set; }

    public string? DescripcionTelefono { get; set; }

    public bool? Estado { get; set; }
}
