using System;
using System.Collections.Generic;

namespace Domain.Entities.Views;

public partial class vw_FacturacionPorFecha
{
    public string ID_Factura { get; set; } = null!;

    public decimal? MontoTotal_Fa { get; set; }

    public DateOnly? FechaEmision_Fa { get; set; }

    public string? Estado_Pago { get; set; }

    public string? Nombre_Pac { get; set; }

    public string? Apellido1_Pac { get; set; }

    public string? Apellido2_Pac { get; set; }
}
