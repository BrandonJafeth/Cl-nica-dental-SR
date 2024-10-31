using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class vw_PacientesConTratamientosActivo
{
    public string ID_Paciente { get; set; } = null!;

    public string? Nombre_Pac { get; set; }

    public string? Apellido1_Pac { get; set; }

    public string? Apellido2_Pac { get; set; }

    public string? Nombre_Tra { get; set; }

    public string? Descripcion_Tra { get; set; }

    public DateOnly? Fecha_Tratamiento { get; set; }
}
