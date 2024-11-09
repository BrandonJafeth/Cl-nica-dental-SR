using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Roles_Permiso
{
    public string ID_Roles_Permisos { get; set; } = null!;

    public string? ID_Roles { get; set; }

    public string? ID_Permisos { get; set; }

    public virtual Permiso? ID_PermisosNavigation { get; set; }

    public virtual Role? ID_RolesNavigation { get; set; }
}
