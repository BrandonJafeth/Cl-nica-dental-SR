using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Usuario
{
    public string ID_Usuario { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Apellido1 { get; set; }

    public string? Apellido2 { get; set; }

    public string? Email { get; set; }

    public string? Contraseña { get; set; }

    public string? Token { get; set; }

    public string? ID_Funcionario { get; set; }

    public virtual Funcionario? ID_FuncionarioNavigation { get; set; }

    public virtual ICollection<Usuario_Role> Usuario_Roles { get; set; } = new List<Usuario_Role>();
}
