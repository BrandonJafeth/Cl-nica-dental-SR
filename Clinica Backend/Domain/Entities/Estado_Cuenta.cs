using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Estado_Cuenta
{
    public string ID_Estado_Cuenta { get; set; } = null!;

    public string? Nombre_EC { get; set; }

    public string? Descripcion_EC { get; set; }

    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();
}
