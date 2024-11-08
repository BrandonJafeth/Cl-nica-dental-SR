using System;
using System.Collections.Generic;

namespace Domain.Entities.Views;

public partial class vw_DentistasConMasTratamiento
{
    public string ID_Dentista { get; set; } = null!;

    public string? Nombre_Den { get; set; }

    public string? Apellido1_Den { get; set; }

    public string? Apellido2_Den { get; set; }

    public int? TotalTratamientos { get; set; }
}
