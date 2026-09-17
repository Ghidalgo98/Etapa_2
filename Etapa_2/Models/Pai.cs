using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class Pai
{
    public int IdPais { get; set; }

    public string DescripcionPais { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Direccion> Direccions { get; set; } = new List<Direccion>();

    public virtual ICollection<Nacionalidad> Nacionalidads { get; set; } = new List<Nacionalidad>();

    public virtual ICollection<PersonaJuridica> PersonaJuridicas { get; set; } = new List<PersonaJuridica>();

    public virtual ICollection<Provincium> Provincia { get; set; } = new List<Provincium>();
}
