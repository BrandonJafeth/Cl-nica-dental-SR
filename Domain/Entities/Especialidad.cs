using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Especialidad
{
    public string ID_Especialidad { get; set; } = null!;

    public string? Nombre_Esp { get; set; }

    public string? Descripcion_Esp { get; set; }

    public virtual ICollection<Dentista_Especialidad> Dentista_Especialidads { get; set; } = new List<Dentista_Especialidad>();
}
