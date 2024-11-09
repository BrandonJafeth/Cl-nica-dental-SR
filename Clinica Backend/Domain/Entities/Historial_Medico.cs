using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Historial_Medico
{
    public string ID_HistorialMedico { get; set; } = null!;

    public DateOnly? Fecha_Historial { get; set; }

    public string? Diagnostico { get; set; }

    public string? ID_Paciente { get; set; }

    public virtual ICollection<Historial_Tratamiento> Historial_Tratamientos { get; set; } = new List<Historial_Tratamiento>();

    public virtual Paciente? ID_PacienteNavigation { get; set; }
}
