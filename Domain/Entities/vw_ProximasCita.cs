﻿using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class vw_ProximasCita
{
    public string ID_Cita { get; set; } = null!;

    public DateOnly? Fecha_Cita { get; set; }

    public string? Motivo { get; set; }

    public TimeOnly? Hora_Inicio { get; set; }

    public TimeOnly? Hora_Fin { get; set; }

    public string? Nombre_Pac { get; set; }

    public string? Apellido1_Pac { get; set; }

    public string? Apellido2_Pac { get; set; }

    public string? Nombre_Den { get; set; }

    public string? Apellido1_Den { get; set; }

    public string? Apellido2_Den { get; set; }
}
