using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Factura_Tratamiento
{
    public string ID_Factura_Tratamiento { get; set; } = null!;

    public string? ID_Factura { get; set; }

    public string? ID_Tratamiento { get; set; }

    public virtual Factura? ID_FacturaNavigation { get; set; }

    public virtual Tratamiento? ID_TratamientoNavigation { get; set; }
}
