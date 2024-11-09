using System;
using System.Collections.Generic;

namespace Domain.Entities.Views;

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
