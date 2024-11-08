using System;
using System.Collections.Generic;

namespace Domain.Entities.Views;

public partial class vw_TratamientosPorPaciente
{
    public string ID_Paciente { get; set; } = null!;

    public string? Nombre_Pac { get; set; }

    public string? Apellido1_Pac { get; set; }

    public string? Apellido2_Pac { get; set; }

    public int? TotalTratamientos { get; set; }
}
