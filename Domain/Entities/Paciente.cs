using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Paciente
{
    public string ID_Paciente { get; set; } = null!;

    public string? Nombre_Pac { get; set; }

    public string? Apellido1_Pac { get; set; }

    public string? Apellido2_Pac { get; set; }

    public DateOnly? Fecha_Nacimiento_Pac { get; set; }

    public string? Telefono_Pac { get; set; }

    public string? Correo_Pac { get; set; }

    public string? Direccion_Pac { get; set; }

    public string? ID_HistorialMedico { get; set; }

    public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();

    public virtual ICollection<Cuentum> Cuenta { get; set; } = new List<Cuentum>();

    public virtual Historial_Medico? ID_HistorialMedicoNavigation { get; set; }
}
