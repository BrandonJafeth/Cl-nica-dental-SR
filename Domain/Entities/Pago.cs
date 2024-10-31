using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Pago
{
    public int ID_Pago { get; set; }

    public decimal? Monto_Pago { get; set; }

    public DateOnly? Fecha_Pago { get; set; }

    public string? ID_Factura { get; set; }

    public string? ID_Tipo_Pago { get; set; }

    public virtual Factura? ID_FacturaNavigation { get; set; }

    public virtual Tipo_Pago? ID_Tipo_PagoNavigation { get; set; }
}
