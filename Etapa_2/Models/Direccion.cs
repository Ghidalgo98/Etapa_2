using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class Direccion
{
    public int IdDireccion { get; set; }

    public long IdPersonaDireccion { get; set; }

    public int Pais { get; set; }

    public int Provincia { get; set; }

    public int Canton { get; set; }

    public int Distrito { get; set; }

    public virtual PersonaJuridica IdPersonaDireccionNavigation { get; set; } = null!;

    public virtual Pai PaisNavigation { get; set; } = null!;
}
