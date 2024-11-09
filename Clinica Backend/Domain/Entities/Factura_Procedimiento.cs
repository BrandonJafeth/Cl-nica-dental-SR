using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Factura_Procedimiento
{
    public string ID_Factura_Procedimiento { get; set; } = null!;

    public string? ID_Factura { get; set; }

    public string? ID_Procedimiento { get; set; }

    public virtual Factura? ID_FacturaNavigation { get; set; }

    public virtual Procedimiento? ID_ProcedimientoNavigation { get; set; }
}
