using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class vw_ResumenFinancieroPaciente
{
    public string ID_Paciente { get; set; } = null!;

    public string? Nombre_Pac { get; set; }

    public string? Apellido1_Pac { get; set; }

    public string? Apellido2_Pac { get; set; }

    public decimal? TotalFacturado { get; set; }

    public decimal? TotalPagado { get; set; }

    public decimal? SaldoPendiente { get; set; }
}
