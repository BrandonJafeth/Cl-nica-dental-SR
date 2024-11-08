using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Historial_Tratamiento
{
    public string ID_Historial_Tratamiento { get; set; } = null!;

    public string ID_HistorialMedico { get; set; } = null!;

    public string ID_Tratamiento { get; set; } = null!;

    public DateOnly? Fecha_Tratamiento { get; set; }

    public virtual Historial_Medico ID_HistorialMedicoNavigation { get; set; } = null!;

    public virtual Tratamiento ID_TratamientoNavigation { get; set; } = null!;
}
