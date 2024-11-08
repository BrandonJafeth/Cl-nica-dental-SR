using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Permiso
{
    public string ID_Permisos { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Roles_Permiso> Roles_Permisos { get; set; } = new List<Roles_Permiso>();
}
