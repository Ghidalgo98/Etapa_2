using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class PersonaJuriducaCorreo
{
    public int CorreoIdCorreo { get; set; }

    public long PersonaJuridicaCedulaJuridica { get; set; }

    public virtual Correo CorreoIdCorreoNavigation { get; set; } = null!;

    public virtual PersonaJuridica PersonaJuridicaCedulaJuridicaNavigation { get; set; } = null!;
}
