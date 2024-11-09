using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Dentista_Especialidad
{
    public string ID_Dentista_Especialidad { get; set; } = null!;

    public string? ID_Dentista { get; set; }

    public string? ID_Especialidad { get; set; }

    public virtual Dentista? ID_DentistaNavigation { get; set; }

    public virtual Especialidad? ID_EspecialidadNavigation { get; set; }
}
