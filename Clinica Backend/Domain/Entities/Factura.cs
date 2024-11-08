using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Factura
{
    public string ID_Factura { get; set; } = null!;

    public decimal? MontoTotal_Fa { get; set; }

    public DateOnly? FechaEmision_Fa { get; set; }

    public string? ID_EstadoPago { get; set; }

    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();

    public virtual ICollection<Factura_Procedimiento> Factura_Procedimientos { get; set; } = new List<Factura_Procedimiento>();

    public virtual ICollection<Factura_Tratamiento> Factura_Tratamientos { get; set; } = new List<Factura_Tratamiento>();

    public virtual Estado_Pago? ID_EstadoPagoNavigation { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
