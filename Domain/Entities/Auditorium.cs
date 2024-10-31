using System;
using System.Collections.Generic;

namespace Clinica_Dental;

public partial class Auditorium
{
    public string ID_Auditoria { get; set; } = null!;

    public DateTime? Fecha_Hora_Accion { get; set; }

    public string? Descripcion_Accion { get; set; }

    public string? DispositivoQueRealizo { get; set; }

    public string? ID_TipoAccion { get; set; }

    public string? ID_Usuario { get; set; }

    public string? ID_DBUser { get; set; }

    public virtual DB_User? ID_DBUserNavigation { get; set; }

    public virtual Tipo_Accion? ID_TipoAccionNavigation { get; set; }

    public virtual Usuario? ID_UsuarioNavigation { get; set; }
}
