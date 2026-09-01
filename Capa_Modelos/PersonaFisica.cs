using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Capa_Modelos;

public partial class PersonaFisica
{
    public long Id { get; set; }

    [NotMapped]
    public int? IdOriginal { get; set; }

    public int Tipo { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido1 { get; set; } = null!;

    public string Apellido2 { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public int Sexo { get; set; }

    public int Nacionalidad { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual Nacionalidad NacionalidadNavigation { get; set; } = null!;

    public virtual ICollection<PersonaFisicaCorreo> PersonaFisicaCorreos { get; set; } = new List<PersonaFisicaCorreo>();

    public virtual SexoPersona SexoNavigation { get; set; } = null!;

    public virtual ICollection<Telefono> Telefonos { get; set; } = new List<Telefono>();

    public virtual TipoPersonaFisica TipoNavigation { get; set; } = null!;

    public virtual Usuario? Usuario { get; set; }
}
