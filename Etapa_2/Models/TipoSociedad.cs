using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class TipoSociedad
{
    public int IdTipoSociedad { get; set; }

    public string DescripcionTipoSociedad { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual ICollection<PersonaJuridica> PersonaJuridicas { get; set; } = new List<PersonaJuridica>();
}
