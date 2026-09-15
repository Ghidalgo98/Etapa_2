using System;
using System.Collections.Generic;

namespace Etapa_1.Models;

public partial class Usuario
{
    public long PersonaFisicaId { get; set; }

    public string Usuario1 { get; set; } = null!;

    public byte[] Contraseña { get; set; } = null!;

    public int EmpleadoIdColaborador { get; set; }

    public bool Estado { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime UltimoAcceso { get; set; }

    public int IntentosFallidos { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public virtual ICollection<AccesoUsuario> AccesoUsuarios { get; set; } = new List<AccesoUsuario>();

    public virtual Empleado EmpleadoIdColaboradorNavigation { get; set; } = null!;

    public virtual PersonaFisica PersonaFisica { get; set; } = null!;
}
