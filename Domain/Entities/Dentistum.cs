using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Dentistum
{
    public string ID_Dentista { get; set; } = null!;

    public string? Nombre_Den { get; set; }

    public string? Apellido1_Den { get; set; }

    public string? Apellido2_Den { get; set; }

    public string? Direccion_Den { get; set; }

    public DateOnly? FechaNacimiento_Den { get; set; }

    public string? Telefono_Den { get; set; }

    public string? Correo_Den { get; set; }

    public string? ID_Funcionario { get; set; }

    public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();

    public virtual ICollection<Dentista_Especialidad> Dentista_Especialidads { get; set; } = new List<Dentista_Especialidad>();

    public virtual Funcionario? ID_FuncionarioNavigation { get; set; }
}
