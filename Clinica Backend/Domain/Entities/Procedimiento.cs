using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Procedimiento
{
    public string ID_Procedimiento { get; set; } = null!;

    public DateOnly? Fecha_Proc { get; set; }

    public string? Detalles_Proc { get; set; }

    public TimeOnly? Hora_Inicio_Proc { get; set; }

    public TimeOnly? Hora_Fin_Proc { get; set; }

    public string? ID_Tratamiento { get; set; }

    public string? ID_Paciente { get; set; }

    public virtual ICollection<Factura_Procedimiento> Factura_Procedimientos { get; set; } = new List<Factura_Procedimiento>();

    public virtual Paciente? ID_PacienteNavigation { get; set; }

    public virtual Tratamiento? ID_TratamientoNavigation { get; set; }
}
