using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Estado_Pago
{
    public string ID_EstadoPago { get; set; } = null!;

    public string? Nombre_EP { get; set; }

    public string? Descripcion_EP { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
