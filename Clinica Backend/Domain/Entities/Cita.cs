using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Cita
{
    public string ID_Cita { get; set; } = null!;

    public DateOnly? Fecha_Cita { get; set; }

    public string? Motivo { get; set; }

    public TimeOnly? Hora_Inicio { get; set; }

    public TimeOnly? Hora_Fin { get; set; }

    public string? ID_Paciente { get; set; }

    public string? ID_Dentista { get; set; }

    public string? ID_Funcionario { get; set; }

    public string? ID_EstadoCita { get; set; }

    public virtual Dentista? ID_DentistaNavigation { get; set; }

    public virtual Estado_Cita? ID_EstadoCitaNavigation { get; set; }

    public virtual Funcionario? ID_FuncionarioNavigation { get; set; }

    public virtual Paciente? ID_PacienteNavigation { get; set; }
}
