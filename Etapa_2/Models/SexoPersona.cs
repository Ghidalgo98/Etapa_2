using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class SexoPersona
{
    public int IdSexoPersona { get; set; }

    public string DescripcionSexoPersona { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<PersonaFisica> PersonaFisicas { get; set; } = new List<PersonaFisica>();
}
