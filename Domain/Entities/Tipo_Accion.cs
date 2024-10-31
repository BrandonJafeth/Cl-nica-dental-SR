using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Tipo_Accion
{
    public string ID_TipoAccion { get; set; } = null!;

    public string? Nombre_Accion { get; set; }

    public string? Descripcion_Tipo_Accion { get; set; }

    public virtual ICollection<Auditorium> Auditoria { get; set; } = new List<Auditorium>();
}
