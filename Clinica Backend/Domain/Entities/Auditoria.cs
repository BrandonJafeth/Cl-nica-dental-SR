using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Auditoria
{
    public Guid ID_Auditoria { get; set; }

    public DateTime Fecha_Hora_Accion { get; set; }

    public string Accion { get; set; } = null!;

    public string DispositivoQueRealizo { get; set; } = null!;

    public string Usuario { get; set; } = null!;
}
