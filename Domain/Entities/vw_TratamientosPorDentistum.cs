using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class vw_TratamientosPorDentistum
{
    public string ID_Dentista { get; set; } = null!;

    public string? Nombre_Den { get; set; }

    public string? Apellido1_Den { get; set; }

    public string? Apellido2_Den { get; set; }

    public string ID_Historial_Tratamiento { get; set; } = null!;

    public DateOnly? Fecha_Tratamiento { get; set; }

    public string? Nombre_Tra { get; set; }

    public string? Descripcion_Tra { get; set; }

    public string? Nombre_Pac { get; set; }

    public string? Apellido1_Pac { get; set; }

    public string? Apellido2_Pac { get; set; }
}
