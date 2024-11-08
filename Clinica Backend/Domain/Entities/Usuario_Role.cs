using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Usuario_Role
{
    public string ID_Usuario_Roles { get; set; } = null!;

    public string? ID_Usuario { get; set; }

    public string? ID_Roles { get; set; }

    public virtual Role? ID_RolesNavigation { get; set; }

    public virtual Usuario? ID_UsuarioNavigation { get; set; }
}
