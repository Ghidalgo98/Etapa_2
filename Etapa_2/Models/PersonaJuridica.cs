using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class PersonaJuridica
{
    public long IdPersonaJuridica { get; set; }

    public string RazonSocial { get; set; } = null!;

    public string NombreComercial { get; set; } = null!;

    public int TipoSociedad { get; set; }

    public long CedulaJuridica { get; set; }

    public DateOnly FechaConstitucion { get; set; }

    public int PaisConstitucion { get; set; }

    public int Categoria { get; set; }

    public bool Estado { get; set; }

    public virtual Proveedor CedulaJuridicaNavigation { get; set; } = null!;

    public virtual ICollection<Direccion> Direccions { get; set; } = new List<Direccion>();

    public virtual Pai PaisConstitucionNavigation { get; set; } = null!;

    public virtual ICollection<PersonaJuriducaCorreo> PersonaJuriducaCorreos { get; set; } = new List<PersonaJuriducaCorreo>();

    public virtual TipoSociedad TipoSociedadNavigation { get; set; } = null!;
}
