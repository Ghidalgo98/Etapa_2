using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class TelefonosPersona
{
    public int TelefonoIdTelefono { get; set; }

    public long PersonaFisicaId { get; set; }

    public virtual PersonaFisica PersonaFisica { get; set; } = null!;

    public virtual Telefono TelefonoIdTelefonoNavigation { get; set; } = null!;
}
