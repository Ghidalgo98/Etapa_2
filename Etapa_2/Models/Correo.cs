using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class Correo
{
    public int IdCorreo { get; set; }

    public string? DescripcionCorreoPersona { get; set; }

    public bool? Estado { get; set; }

    public virtual PersonaFisicaCorreo? PersonaFisicaCorreo { get; set; }

    public virtual PersonaJuriducaCorreo? PersonaJuriducaCorreo { get; set; }

    public virtual ICollection<Proveedor> ProveedorIdProveedors { get; set; } = new List<Proveedor>();
}
