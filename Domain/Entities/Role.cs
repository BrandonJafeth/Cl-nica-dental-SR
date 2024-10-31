using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Role
{
    public string ID_Roles { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Roles_DBUser> Roles_DBUsers { get; set; } = new List<Roles_DBUser>();

    public virtual ICollection<Roles_Permiso> Roles_Permisos { get; set; } = new List<Roles_Permiso>();

    public virtual ICollection<Usuario_Role> Usuario_Roles { get; set; } = new List<Usuario_Role>();
}
