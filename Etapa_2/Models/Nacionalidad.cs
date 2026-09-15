using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class Nacionalidad
{
    public int IdNacionalidad { get; set; }

    public string DescripcionNacionalidad { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<PersonaFisica> PersonaFisicas { get; set; } = new List<PersonaFisica>();
}
