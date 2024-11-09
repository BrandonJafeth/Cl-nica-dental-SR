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

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();

    public virtual Historial_Medico? Historial_Medico { get; set; }

    public virtual ICollection<Procedimiento> Procedimientos { get; set; } = new List<Procedimiento>();
}
