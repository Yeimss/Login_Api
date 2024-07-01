using System;
using System.Collections.Generic;

namespace RegistroDVP_Api.Models.DB;

public partial class Persona
{
    public int Identificador { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public string? NumeroIdentificacion { get; set; }

    public string? Email { get; set; }

    public string? TipoIdentificacion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? IdentificacionCompleta { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public virtual Usuario? Usuario { get; set; }
}
