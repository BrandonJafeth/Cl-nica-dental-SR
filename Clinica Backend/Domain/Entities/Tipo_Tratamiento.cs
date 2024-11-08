using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Tipo_Tratamiento
{
    public string ID_TipoTratamiento { get; set; } = null!;

    public string? Nombre_Tipo_Tratamiento { get; set; }

    public string? Descripcion_Tipo_Tratamiento { get; set; }

    public virtual ICollection<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();
}
