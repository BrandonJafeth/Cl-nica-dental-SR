using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Tratamiento
{
    public string ID_Tratamiento { get; set; } = null!;

    public string? Nombre_Tra { get; set; }

    public string? Descripcion_Tra { get; set; }

    public string? ID_TipoTratamiento { get; set; }

    public virtual ICollection<Factura_Tratamiento> Factura_Tratamientos { get; set; } = new List<Factura_Tratamiento>();

    public virtual ICollection<Historial_Tratamiento> Historial_Tratamientos { get; set; } = new List<Historial_Tratamiento>();

    public virtual Tipo_Tratamiento? ID_TipoTratamientoNavigation { get; set; }

    public virtual ICollection<Procedimiento> Procedimientos { get; set; } = new List<Procedimiento>();
}
