using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Funcionario
{
    public string ID_Funcionario { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Apellido1 { get; set; }

    public string? Apellido2 { get; set; }

    public string? Email { get; set; }

    public string? Contraseña { get; set; }

    public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();

    public virtual ICollection<Dentistum> Dentista { get; set; } = new List<Dentistum>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
