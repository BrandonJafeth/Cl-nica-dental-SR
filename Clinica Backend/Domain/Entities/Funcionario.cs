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

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual ICollection<Dentista> Dentista { get; set; } = new List<Dentista>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
