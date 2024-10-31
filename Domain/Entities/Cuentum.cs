using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Cuentum
{
    public string ID_Cuenta { get; set; } = null!;

    public decimal? Saldo_Total { get; set; }

    public DateOnly? Fecha_Apertura { get; set; }

    public DateOnly? Fecha_Cierre { get; set; }

    public DateOnly? Fecha_Ultima_Actualizacion { get; set; }

    public string? Observaciones { get; set; }

    public string? ID_Estado_Cuenta { get; set; }

    public string? ID_Factura { get; set; }

    public string? ID_Paciente { get; set; }

    public virtual Estado_Cuentum? ID_Estado_CuentaNavigation { get; set; }

    public virtual Factura? ID_FacturaNavigation { get; set; }

    public virtual Paciente? ID_PacienteNavigation { get; set; }
}
