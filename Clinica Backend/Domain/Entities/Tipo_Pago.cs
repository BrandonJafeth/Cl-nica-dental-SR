using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Tipo_Pago
{
    public string ID_Tipo_Pago { get; set; } = null!;

    public string? Nombre_TP { get; set; }

    public string? Descripcion_TP { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
