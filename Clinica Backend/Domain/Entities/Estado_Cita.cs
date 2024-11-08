using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Estado_Cita
{
    public string ID_EstadoCita { get; set; } = null!;

    public string? Nombre_Estado { get; set; }

    public string? Descripcion_Estado { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
}
