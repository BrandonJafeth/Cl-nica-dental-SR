using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Estado_Tratamiento
{
    public string ID_EstadoTratamiento { get; set; } = null!;

    public string? Nombre_Estado { get; set; }

    public string? Descripcion_Estado { get; set; }

    public virtual ICollection<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();
}
