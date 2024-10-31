using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Historial_Medico
{
    public string ID_HistorialMedico { get; set; } = null!;

    public DateOnly? Fecha_Historial { get; set; }

    public string? Diagnostico { get; set; }

    public string? Tratamientos_Medicos { get; set; }

    public virtual ICollection<Historial_Tratamiento> Historial_Tratamientos { get; set; } = new List<Historial_Tratamiento>();

    public virtual ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
}
