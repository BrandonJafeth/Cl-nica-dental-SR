using System;
using System.Collections.Generic;

namespace Domain.Entities.Views;

public partial class vw_TratamientosPorDentista
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
