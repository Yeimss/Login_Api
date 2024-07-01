using System;
using System.Collections.Generic;

namespace RegistroDVP_Api.Models.DB;

public partial class Usuario
{
    public int Identificador { get; set; }

    public string? Usuario1 { get; set; }

    public string? Pass { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual Persona IdentificadorNavigation { get; set; } = null!;
}
